using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.Pl.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string FolderName)
        {
            //1. Get Located Folder Path
            //var FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/files/" + FolderName;
            
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", FolderName);

            //2. Get File Name And Make its name unique
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.Name)}";
           string extenstion= Path.GetExtension(file.FileName);
            string fileNameWithExtenction = fileName + extenstion;
            //3.Get File Path
            var FilePath =Path.Combine(FolderPath, fileNameWithExtenction);

            //4.Save file
             using var fs = new FileStream(FilePath , FileMode.Create);

            file.CopyTo(fs);

            return fileNameWithExtenction;
        }

        public static void Delete(string fileName , string FolderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/files",FolderName,fileName);

            if(File.Exists(FilePath)) File.Delete(FilePath);
        }
    }
}
