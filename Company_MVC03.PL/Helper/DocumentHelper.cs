using static NuGet.Packaging.PackagingConstants;

namespace Company_MVC03.PL.Helper
{
    public static class DocumentHelper
    {

        // 1. Upload File

        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get the folder path
            //string folderPath = "D:\\Courses\\Asp.Net(Rabaa)\\Projects\\07 MVC\\Company_MVC03\\Company_MVC03.PL\\wwwroot\files\\" + folderName;
            //var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + folderName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // 2. Get File Name And Make It Unique

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // File Path

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);


            return fileName;

            /* // Copilot
            // Check if the file is not null and has content
            if (file != null && file.Length > 0)
            {
                // Generate a unique file name to avoid conflicts
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                // Combine the folder path with the unique file name
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, uniqueFileName);
                // Ensure the directory exists
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                // Return the unique file name for storage in the database
                return uniqueFileName;
            }
            // Return null if no file was uploaded
            return null;
            */
        }

        // 2. Delete File
        public static void DeleteFile(string fileName, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName, fileName);
            if (File.Exists(folderPath))
            {
                File.Delete(folderPath);
            }
        }

    }
}
