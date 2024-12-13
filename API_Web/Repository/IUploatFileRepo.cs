namespace API_Web.Repository
{
    public interface IUploatFileRepo
    {
        Task<string> UpLoatFile(IFormFile file);
    }
}
