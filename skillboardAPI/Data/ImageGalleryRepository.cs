using skillboardAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using skillboardAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace skillboardAPI.Data
{
    public class ImageGalleryRepository : IImageGalleryRepository
    {
        private readonly SkillContext _context = null;
        public ImageGalleryRepository(IOptions<Settings> settings)
        {
            _context = new SkillContext(settings);
        }

        public async Task AddImage(ImageGallery item)
        {
            try
            {
                await _context.ImageGallery.InsertOneAsync(item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ImageGallery>> GetAllImages()
        {
            try
            {
                return await _context.ImageGallery.Find(_ => true).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ImageGallery> GetImage(string name)
        {
            var filter = Builders<ImageGallery>.Filter.Eq("Title", name);
            try
            {

                return await _context.ImageGallery
                    .Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DeleteResult> RemoveAllImages()
        {
            try
            {
                return await _context.ImageGallery.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<DeleteResult> RemoveImage(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateImage(string id, string description)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> UpdateImageDocument(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}
