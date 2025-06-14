namespace Ecomerce.backend.Services
{
    public interface IFilesService
    {
        Task<string> UploadImage(string imageBase64);

    }
}
