namespace SimpleWebServer.WebFunction
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.AspNetCore.StaticFiles;

    public class ImageApi : IRestApi
    {
        public void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/images",
                (ImageService service) =>
                {
                    return Results.Ok(service.GetImages());
                });

            endpoints.MapGet("/api/images/{fileName}",
                (string fileName,
                 ImageService service) =>
                {
                    Stream stream = service.OpenImage(fileName);

                    if (stream is null)
                        return Results.NotFound();

                    FileExtensionContentTypeProvider provider = new();

                    if (!provider.TryGetContentType(fileName, out string contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    return Results.File(
                        stream,
                        contentType,
                        enableRangeProcessing: true);
                });
        }
    }
}
