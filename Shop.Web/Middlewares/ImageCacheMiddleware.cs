using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Shop.Web.Middlewares
{
    public class ImageCacheMiddleware
    {
        private ImageCacheOptions _options;

        private readonly RequestDelegate _next;

        public ImageCacheMiddleware(RequestDelegate next, ImageCacheOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("image"))
            {
                Stream originalBody = context.Response.Body;

                try
                {
                    using (var memStream = new MemoryStream())
                    {
                        var imageName = $"{context.Request.RouteValues["id"]}.jpg";

                        var imagePaths = Directory.GetFiles(_options.Path);
                        var imageNames = imagePaths.Select(x => Path.GetFileName(x));
                   
                        if (imageNames.Contains(imageName))
                        {
                            byte[] fileBytes = File.ReadAllBytes(_options.Path + "/" + imageName);
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "image/jpeg";
                            await context.Response.Body.WriteAsync(fileBytes, 0, fileBytes.Length);
                        }
                        else
                        {
                            if (imagePaths.Length < _options.MaxCount)
                            {
                                context.Response.Body = memStream;

                                await _next(context);

                                if (context.Response.ContentType != null && context.Response.ContentType.Contains("image/jpeg"))
                                {
                                    if(memStream.Length > 0)
                                    {
                                        memStream.Position = 0;
                                        var fileName = context.Request.RouteValues["id"];
                                        var filePath = Path.Combine(_options.Path, $"{fileName}.jpg");
                                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                                        {
                                            memStream.CopyTo(fileStream);
                                        }

                                        memStream.Position = 0;
                                        await memStream.CopyToAsync(originalBody);
                                    }
                                }
                            }
                            else
                            {
                                await _next(context);
                            }
                        }
                    }

                }
                catch(Exception ex)
                {
                    Log.Error(ex.Message);
                }
                finally
                {
                    context.Response.Body = originalBody;
                } 
            }
            else if(context.Request.Method == "POST" && context.Request.Path.Value.Contains("categories/edit", StringComparison.CurrentCultureIgnoreCase)) {
                var imageName = $"{context.Request.RouteValues["id"]}.jpg";

                var imagePaths = Directory.GetFiles(_options.Path);
                var imageNames = imagePaths.Select(x => Path.GetFileName(x));

                if (imageNames.Contains(imageName))
                {
                    File.Delete(Path.Combine(_options.Path, $"{imageName}"));
                }
                await _next(context);
            }
            else
            {
                await _next(context);
            }
        }
    }

    public class ImageCacheOptions
    {
        public ImageCacheOptions(string path, int maxCount = 10)
        {
            this.Path = path;
            this.MaxCount = maxCount;
        }

        public string Path { get; set; }

        public int MaxCount { get; set; }
    }
}
