using CloudinaryDotNet.Actions;
using vniu_api.ViewModels.UtilsViewModels;

namespace vniu_api.Repositories.Utils
{
    public interface IPhotoService
    {
        public Task<PhotoVM> CreatePhotoAsync(IFormFile file);
        public Task<PhotoVM> DeletePhotoAsync(int publicId);
    }
}
