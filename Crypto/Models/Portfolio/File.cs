namespace Crypto.Models.Portfolio
{
    public class File : IFile
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
