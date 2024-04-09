using CloudinaryDotNet.Actions;
using vniu_api.ViewModels.UtilsViewModels;

namespace vniu_api.Repositories.Utils
{
    public interface IPhotoService
    {
        public Task<PhotoVM> CreatePhotoAsync(IFormFile file);
        public Task<ICollection<PhotoVM>> CreatePhotosAsync(ICollection<IFormFile> files);
        public Task<PhotoVM> DeletePhotoAsync(int publicId);
        public Task<ICollection<PhotoVM>> DeletePhotosAsync(ICollection<int> publicIds);
    }
}
