namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;

    public class ImageService
    {
        private readonly WebServerConfiguration _configuration;

        public ImageService(WebServerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<ImageInfo> GetImages()
        {
            if (Directory.Exists(_configuration.ImagePath) == false)
            {
                return [];
            }

            if (Environment.UserDomainName == "PTA")
            {
                _configuration.ImagePath = @"c:\Temp\Screenshot\";
            }

            return Directory
                .EnumerateFiles(_configuration.ImagePath)
                .Where(IsSupportedImage)
                .Select(CreateImageInfo)
                .OrderByDescending(i => i.LastWriteTime)
                .ToList();
        }

        public ImageInfo GetImage(string fileName)
        {
            if (IsValidFileName(fileName) == false)
            {
                return null;
            }

            string fullName = GetFullFileName(fileName);

            if (!File.Exists(fullName))
                return null;

            return CreateImageInfo(fullName);
        }

        public Stream OpenImage(string fileName)
        {
            if (IsValidFileName(fileName) == false)
            {
                return null;
            }

            string fullName = GetFullFileName(fileName);

            if (!File.Exists(fullName))
                return null;

            return File.OpenRead(fullName);
        }

        private string GetFullFileName(string fileName)
        {
            return Path.Combine(
                _configuration.ImagePath,
                fileName);
        }

        private static bool IsSupportedImage(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            return extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".bmp", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".gif", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".webp", StringComparison.OrdinalIgnoreCase);
        }

        private static ImageInfo CreateImageInfo(string fileName)
        {
            FileInfo fileInfo = new(fileName);

            return new ImageInfo
            {
                Name = fileInfo.Name,
                LastWriteTime = fileInfo.LastWriteTime,
                Size = fileInfo.Length
            };
        }

        private static bool IsValidFileName(string fileName)
        {
            return fileName == Path.GetFileName(fileName);
        }

        public ImageDirectoryState GetState()
        {
            if (Environment.UserDomainName == "PTA")
            {
                _configuration.ImagePath = @"c:\Temp\Screenshot\";
            }

            if (!Directory.Exists(_configuration.ImagePath))
            {
                return new ImageDirectoryState(0, DateTime.MinValue);
            }

            FileInfo[] files = Directory.EnumerateFiles(_configuration.ImagePath).Where(IsSupportedImage).Select(file => new FileInfo(file)).ToArray();

            if (files.Length == 0)
            {
                return new ImageDirectoryState(0, DateTime.MinValue);
            }

            return new ImageDirectoryState(files.Length, files.Max(f => f.LastWriteTime));
        }
    }
}
