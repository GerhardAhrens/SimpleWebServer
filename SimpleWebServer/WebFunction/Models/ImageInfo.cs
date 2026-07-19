namespace SimpleWebServer.WebFunction
{
    using System;

    public class ImageInfo
    {
        public string Name { get; init; } = string.Empty;

        public DateTime LastWriteTime { get; init; }

        public long Size { get; init; }
    }
}
