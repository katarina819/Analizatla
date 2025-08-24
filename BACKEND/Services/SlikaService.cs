using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BACKEND.Services
{
    public class SlikaService
    {
        private readonly IWebHostEnvironment _env;

        public SlikaService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string SpremiSliku(string base64, string fileName)
        {
            var folder = Path.Combine(_env.WebRootPath, "slike");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var bytes = Convert.FromBase64String(base64);
            var filePath = Path.Combine(folder, fileName);

            File.WriteAllBytes(filePath, bytes);

            return $"/slike/{fileName}";
        }
    }
}
