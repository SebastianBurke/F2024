namespace scbH60Store.DAL
{
    public class ImageService : IImageService
    {
        public async Task<string> SaveImageAsync(IFormFile imageFile, string defaultImagePath, string existingImageUrl = null)
        {
            // Define the default image path if no image is uploaded
            var defaultImage = "/images/default-image.png";

            // If a new image is uploaded, save it and delete the old image if necessary
            if (imageFile != null && imageFile.Length > 0)
            {
                var newImagePath = Path.Combine("wwwroot/images", imageFile.FileName);

                // If an existing image exists and it's not the default, delete it
                if (!string.IsNullOrEmpty(existingImageUrl) && existingImageUrl != defaultImage)
                {
                    var oldImagePath = Path.Combine("wwwroot", existingImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save the new image
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return $"/images/{imageFile.FileName}";
            }

            // If no new image, return the existing one or default image
            return string.IsNullOrEmpty(existingImageUrl) ? defaultImagePath : existingImageUrl;
        }
    }

}
