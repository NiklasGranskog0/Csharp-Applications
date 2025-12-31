using DuplicateFinderApp.Files;

namespace DuplicateFinderTest
{
    [TestClass]
    public sealed class DuplicateFinderTests
    {

        private class TestDirectory : IDirectoryFinder
        {
            public string? Path { get; set; }

            public string? GetFolderPath()
            {
                return Path;
            }
        }

        [TestMethod]
        public async Task DuplicateFinder_BrowseDirectoryAsync_Success()
        {
            // Arrange
            var path = @"C:\Users\";
            var directoryFinder = new TestDirectory { Path = path };
            var duplicateFinder = new DuplicateFinder(directoryFinder);

            // Act
            var report = await duplicateFinder.BrowseDirectoryAsync();

            // Assert
            Assert.IsTrue(report.Success);
            Assert.AreEqual("Directory Set", report.Message);
            Assert.AreEqual(path, report.Value);
        }

        [TestMethod]
        public async Task DuplicateFinder_BrowseDirectoryAsync_Failed()
        {
            // Arrange
            var directoryFinder = new TestDirectory { Path = null };
            var duplicateFinder = new DuplicateFinder(directoryFinder);

            // Act
            var report = await duplicateFinder.BrowseDirectoryAsync();

            // Assert
            Assert.IsFalse(report.Success);
            Assert.AreEqual("Invalid Directory, Keeping previous valid directory selected", report.Message);
        }

        [TestMethod]
        public async Task DuplicateFinder_FindFilesAsync_Success()
        {
            // Arrange
            var config = new Configuration { FolderPath = @"C:\Users\" };
            var tokenSource = new CancellationTokenSource();
            var duplicateFinder = new DuplicateFinder();
            var progress = new Progress<ProgressReport>();

            // Act
            var report = await duplicateFinder.FindFilesAsync(config, tokenSource.Token, progress);

            // Assert
            Assert.IsTrue(report.Success);
            Assert.AreEqual("Finished Search Directory", report.Message);
        }

        [TestMethod]
        public async Task DuplicateFinder_FindFilesAsync_Failed()
        {
            // Arrange
            var config = new Configuration { FolderPath = @"C:\Users\" };
            var tokenSource = new CancellationTokenSource();
            var duplicateFinder = new DuplicateFinder();
            var progress = new Progress<ProgressReport>();

            // Act
            await tokenSource.CancelAsync();
            var report = await duplicateFinder.FindFilesAsync(config, tokenSource.Token, progress);

            // Assert
            Assert.IsFalse(report.Success);
            Assert.AreEqual("Operation Canceled", report.Message);
        }

        [TestMethod]
        public async Task DuplicateFinder_FindDuplicateFilesAsync_Success_NoDuplicateFiles()
        {
            // Arrange
            var duplicateFinder = new DuplicateFinder();

            // Act
            var (files, report) = await duplicateFinder.FindDuplicateFilesAsync();

            // Assert
            Assert.IsTrue(report.Success);
            Assert.AreEqual("No Duplicate Files Found", report.Message);
            Assert.AreEqual(0, files.Count);
        }

        [TestMethod]
        public async Task DuplicateFinder_FindDuplicateFilesAsync_Success()
        {
            // Arrange
            var duplicateFinder = new DuplicateFinder();
            var filesStructs = new List<FileStruct>()
            {
                new()
                {
                    FileName = "test.txt",
                    FullPath = @"C:\Users\test.txt",
                    Size = 100,
                    LastWriteTime = DateTime.Now,
                    CreatedDate = DateTime.Now
                },

                new()
                {
                    FileName = "test.txt",
                    FullPath = @"C:\Users\test.txt",
                    Size = 100,
                    LastWriteTime = DateTime.Now,
                    CreatedDate = DateTime.Now
                },
            };

            // Act
            var (files, report) = await duplicateFinder.FindDuplicateFilesAsync(filesStructs);

            // Assert
            Assert.IsTrue(report.Success);
            Assert.AreEqual("Finished Searching for Duplicate Files", report.Message);
            Assert.AreEqual(2, files.Count);
        }
    }
}
