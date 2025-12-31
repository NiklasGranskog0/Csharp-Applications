using System.Collections.ObjectModel;
using System.IO;
using WindowsAPICodePack.Dialogs;

namespace DuplicateFinderApp.Files
{
    public interface IDirectoryFinder
    {
        string? GetFolderPath();
    }

    public class WindowsDirectorySelector : IDirectoryFinder
    {
        public string? GetFolderPath()
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Title = "Select Directory to Search",
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false
            };

            return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileName : null;
        }
    }

    public class DuplicateFinder
    {
        private TaskReport m_TaskReport;
        private List<FileStruct> m_ListOfFileStructs = [];
        private readonly List<FileStruct> m_ListOfDuplicateFiles = [];
        private Configuration m_Configuration;

        private readonly IDirectoryFinder m_DirectoryFinder;

        public DuplicateFinder() : this(new WindowsDirectorySelector()) { }

        // For testing purposes
        public DuplicateFinder(IDirectoryFinder directoryFinder)
        {
            m_DirectoryFinder = directoryFinder;
        }

        public async Task<TaskReport> BrowseDirectoryAsync()
        {
            ClearVariables();

            var directory = m_DirectoryFinder.GetFolderPath();

            if (directory == null)
            {
                m_TaskReport.Report(false, "Invalid Directory, Keeping previous valid directory selected");
                return m_TaskReport;
            }

            m_TaskReport.Report(true, "Directory Set", directory);
            return m_TaskReport;
        }

        public async Task<TaskReport> FindFilesAsync(
            Configuration config,
            CancellationToken cancellationToken,
            IProgress<ProgressReport> progress)
        {
            ClearVariables();

            m_Configuration = config;
            var report = new ProgressReport();

            var options = new EnumerationOptions()
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true,
            };

            await Task.Run(() =>
            {
                var extensionFilter = config.GetFileExtensions();

                try
                {
                    var allFiles = Directory.GetFiles(config.FolderPath!, "*", options);
                    var totalFiles = allFiles.Length;

                    foreach (var file in allFiles)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        if ((config.AllFiles != true && config.NoFileTypesSelected()) || config.AllFiles == true)
                        {
                            AddFileStruct(new FileInfo(file));

                            report.FileStructs = m_ListOfFileStructs;
                            report.Progress = totalFiles > 0 ? (report.FileStructs.Count * 100) / totalFiles : 0;
                            progress.Report(report);

                            continue;
                        }

                        var extension = Path.GetExtension(file);
                        if (!extensionFilter.Contains(extension))
                            continue;

                        AddFileStruct(new FileInfo(file));

                        report.FileStructs = m_ListOfFileStructs;
                        report.Progress = totalFiles > 0 ? (report.FileStructs.Count * 100) / totalFiles : 0;
                        progress.Report(report);
                    }
                }
                catch (OperationCanceledException e)
                {
                    m_TaskReport.Report(false, "Operation Canceled");
                    return m_TaskReport;
                }

                m_TaskReport.Report(true, "Finished Search Directory");
                return m_TaskReport;
            });

            return m_TaskReport;
        }

        public async Task<(ObservableCollection<FileStruct>, TaskReport)> FindDuplicateFilesAsync(List<FileStruct>? files = null)
        {
            Dictionary<string, List<FileStruct>> filesByKeys = [];

            if (m_Configuration.NoAttributeSelected())
                SetAllAttributes();

            try
            {
                await Task.Run(() =>
                {
                    m_ListOfDuplicateFiles.Clear();

                    // For testing purposes
                    if (files != null) m_ListOfFileStructs = files;

                    foreach (var file in m_ListOfFileStructs)
                    {
                        var key = m_Configuration.GetUniqueKey(file);

                        if (!filesByKeys.TryGetValue(key, out List<FileStruct>? value))
                        {
                            value = [];
                            filesByKeys[key] = value;
                        }

                        value.Add(file);
                    }

                    foreach (var file in filesByKeys.Values.Where(file => file.Count > 1))
                    {
                        m_ListOfDuplicateFiles.AddRange(file);
                    }
                });
            }
            catch (Exception e)
            {
                m_TaskReport.Report(false, e.Message);
                return ([], m_TaskReport);
            }

            if (m_ListOfDuplicateFiles.Count == 0)
            {
                m_TaskReport.Report(true, "No Duplicate Files Found");
                return ([], m_TaskReport);
            }

            m_TaskReport.Report(true, "Finished Searching for Duplicate Files");
            return (new ObservableCollection<FileStruct>(m_ListOfDuplicateFiles), m_TaskReport);
        }

        private void SetAllAttributes()
        {
            m_Configuration.Name = true;
            m_Configuration.Size = true;
            m_Configuration.LastWriteTime = true;
            m_Configuration.CreatedDate = true;
        }

        private void AddFileStruct(in FileInfo fileInfo)
        {
            var newFileStruct = new FileStruct
            {
                FileName = fileInfo.Name,
                FullPath = fileInfo.FullName,
                Size = fileInfo.Length,
                LastWriteTime = fileInfo.LastWriteTime,
                CreatedDate = fileInfo.CreationTime
            };

            m_ListOfFileStructs.Add(newFileStruct);
        }

        private void ClearVariables()
        {
            m_ListOfFileStructs.Clear();
            m_Configuration = new Configuration();
            m_TaskReport.Clear();
        }
    }
}