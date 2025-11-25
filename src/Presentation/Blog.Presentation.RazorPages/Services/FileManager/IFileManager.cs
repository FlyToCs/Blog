namespace Blog.Presentation.RazorPages.Services.FileManager
{
    public interface IFileManager
    {
        string SaveFileAndReturnName(IFormFile file, string savePath);
        void DeleteFile(string fileName, string path);
    }
}
