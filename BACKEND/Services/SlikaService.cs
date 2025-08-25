using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace BACKEND.Services
{
    /// <summary>
    /// Servis za spremanje slika na server.
    /// </summary>
    public class SlikaService
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Konstruktor servisa. Prima instancu <see cref="IWebHostEnvironment"/> kako bi znao putanju do web root foldera.
        /// </summary>
        /// <param name="env">Instanca <see cref="IWebHostEnvironment"/> koja sadrži informacije o web okruženju.</param>
        public SlikaService(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Sprema sliku na server iz Base64 stringa u folder "wwwroot/slike".
        /// </summary>
        /// <param name="base64">Base64 kodirana slika.</param>
        /// <param name="fileName">Ime datoteke s kojom æe slika biti spremljena.</param>
        /// <returns>Vraæa URL slike za pristup preko weba.</returns>
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
