using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using vniu_api.Models.EF.Utils;
using vniu_api.Repositories;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.UtilsViewModels;

namespace vniu_api.Services.Utils
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PhotoService(DataContext context, IMapper mapper, IOptions<CloudinarySettings> config)
        {
            _context = context;
            _mapper = mapper;

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<PhotoVM> CreatePhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file == null || file.Length <= 0)
            {
                throw new Exception("File is empty");
            } 

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    UniqueFilename = true,
                    UseFilename = true,
                    Overwrite = true,
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            };

            var newPhoto = new Photo()
            {
                PhotoPublicId = uploadResult.PublicId,
                PhotoUrl = uploadResult.Url.ToString()
            };

            _context.Photos.Add(newPhoto);

            await _context.SaveChangesAsync();

            // result
            var newPhotoVM = _mapper.Map<PhotoVM>(newPhoto);

            return newPhotoVM;
        }

        public async Task<PhotoVM> DeletePhotoAsync(int photoId)
        {
            var photoDelete = await _context.Photos.FindAsync(photoId);

            if (photoDelete == null)
            {
                throw new Exception("Photo not found");
            }

            // photo exists


            var deleteParams = new DeletionParams(photoDelete.PhotoPublicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Error == null)
            {
                throw new Exception("Remove photo failed");
            }

            // success
            _context.Photos.Remove(photoDelete);

            await _context.SaveChangesAsync();

            // mapping
            var photoDeleteVM = _mapper.Map<PhotoVM>(photoDelete);

            return photoDeleteVM;
        }
    }
}
