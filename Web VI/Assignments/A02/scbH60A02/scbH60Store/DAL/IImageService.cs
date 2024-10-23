using System.Threading.Tasks;

namespace scbH60Store.DAL
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string defaultImagePath, string existingImageUrl = null);
    }
}
