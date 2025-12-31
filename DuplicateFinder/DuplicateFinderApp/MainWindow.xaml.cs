using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DuplicateFinderApp.Files;

namespace DuplicateFinderApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly DuplicateFinder m_DuplicateFinder;
        private Configuration m_Configuration;
        private readonly Progress<ProgressReport> m_Progress;

        private CancellationTokenSource m_CancellationTokenSource = new();
        private ObservableCollection<FileStruct> m_Files;
        public ObservableCollection<FileStruct> Files
        {
            get => m_Files;
            set
            {
                if (Equals(value, m_Files)) return;
                m_Files = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            m_DuplicateFinder = new DuplicateFinder();
            m_Progress = new Progress<ProgressReport>();
            m_Progress.ProgressChanged += ProgressChanged;

            m_Files = [];

            CancelButton.IsEnabled = false;

            DataContext = this;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ProgressChanged(object? sender, ProgressReport e)
        {
            ProgressBar.Value = e.Progress;
            ProgressTextBlock.Text = $"{e.Progress}%";
        }

        private async void BrowseButton_OnClick(object sender, RoutedEventArgs e)
        {
            InitializeProgressBar();
            Files.Clear();

            try
            {
                var taskReport = await m_DuplicateFinder.BrowseDirectoryAsync();
                ShowReportMessage(taskReport);

                if (taskReport.Success)
                    FilePathLabel.Content = taskReport.Value;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void InitializeProgressBar()
        {
            ProgressBar.Value = 0;
            ProgressTextBlock.Text = "0%";
        }

        private async void FindButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SetConfiguration();

                if (!ValidConfiguration()) return;

                DisableButtons(true);
                ShowReportMessage(new TaskReport { Success = true, Message = "Searching for Duplicate Files..." },
                    999);

                if (m_CancellationTokenSource.IsCancellationRequested)
                {
                    m_CancellationTokenSource.Dispose();
                    m_CancellationTokenSource = new CancellationTokenSource();
                }

                var taskReport =
                    await m_DuplicateFinder.FindFilesAsync(m_Configuration, m_CancellationTokenSource.Token,
                        m_Progress);
                ShowReportMessage(taskReport);

                if (taskReport.Success)
                {
                    (Files, taskReport) = await m_DuplicateFinder.FindDuplicateFilesAsync();
                    ShowReportMessage(taskReport);
                }

                DisableButtons(false, true, true);
                InitializeProgressBar();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void DisableButtons(bool cancel = false, bool find = false, bool browse = false)
        {
            CancelButton.IsEnabled = cancel;
            FindButton.IsEnabled = find;
            BrowseButton.IsEnabled = browse;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            m_CancellationTokenSource.Cancel();
            ShowReportMessage(new TaskReport { Success = false, Message = "Operation Cancelling..." });
        }

        private void ShowReportMessage(TaskReport report, long duration = 2)
        {
            FindException.Foreground =
                !report.Success ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);

            var fadeOutAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                BeginTime = TimeSpan.FromSeconds(3)
            };

            FindException.Content = report.Message;
            FindException.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void SetConfiguration()
        {
            m_Configuration = new Configuration
            {
                FolderPath = FilePathLabel.Content.ToString(),

                Name = NameCheckBox.IsChecked,
                LastWriteTime = ModifiedDateCheckBox.IsChecked,
                CreatedDate = CreatedDateCheckBox.IsChecked,
                Size = SizeCheckBox.IsChecked,
                // Hash = HashCheckBox.IsChecked,
                AllFiles = AllFilesCheckBox.IsChecked,
                Pictures = PicturesCheckBox.IsChecked,
                Pdf = PdfCheckBox.IsChecked,
                TextFiles = TextFilesCheckBox.IsChecked,
            };
        }

        private bool ValidConfiguration()
        {
            if (!Directory.Exists(m_Configuration.FolderPath))
            {
                MessageBox.Show("No valid directory selected. Please browse to a valid directory.",
                    "No valid directory selected",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (m_Configuration.NoFileTypesSelected())
            {
                var result = MessageBox.Show("No file types selected. This will default to ALL FILE TYPES.",
                    "No File type selected",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Cancel) return false;
            }

            if (m_Configuration.NoAttributeSelected())
            {
                var result = MessageBox.Show("No attributes selected. This will default to ALL ATTRIBUTES.",
                    "No Attribute selected",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.Cancel) return false;
            }

            return true;
        }


    }
}