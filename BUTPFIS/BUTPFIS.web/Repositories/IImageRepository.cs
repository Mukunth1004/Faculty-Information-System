namespace BUTPFIS.web.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
