using ETicaretAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Local
{

    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string path, string fileName)
        => File.Delete($"{path}\\{fileName}");
        

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f=>f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
        => File.Exists(($"{path}\\{fileName}"));


        //private async Task<string> FileRenameAsync(string path, string fileName, Func<string, string, bool> hasFile)
        //{
        //    string extension = Path.GetExtension(fileName);
        //    string oldName = Path.GetFileNameWithoutExtension(fileName);
        //    Regex regex = new Regex("[*'\",+-._&#^@|/<>~]");
        //    string newFileName = regex.Replace(oldName, string.Empty);
        //    DateTime datetimenow = DateTime.UtcNow;
        //    string datetimeutcnow = datetimenow.ToString("yyyyMMddHHmmss");
        //    string fullName = $"{datetimeutcnow}-{newFileName}{extension}";

        //    return fullName;
        //}
        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);


            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
            string fileNewName = await FileRenameAsync(uploadPath, file.Name,HasFile);

                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
            }

            return datas;
        }
    }
}
                