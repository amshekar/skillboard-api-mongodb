using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skillboardAPI.Interfaces;
using skillboardAPI.Models;
using MongoDB.Bson;
using skillboardAPI.Extensions;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace skillboardAPI.Controllers
{
    [Route("api")]
    public class ImageGalleryController : Controller
    {
        private readonly IImageGalleryRepository _imageGalleryRepository;
        public ImageGalleryController(IImageGalleryRepository imageGalleryRepository)
        {
            _imageGalleryRepository = imageGalleryRepository;
        }


        [HttpGet]
        [Route("imagegallery/{title}")]
        public Task<IActionResult> GetGalleryByName(string title)
        {
            return GetGalleryByNameInternal(title);


        }

        private async Task<IActionResult> GetGalleryByNameInternal(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var result = await _imageGalleryRepository.GetImage(name);
            if (result != null)
            {
                return Ok(result);
            }
            else
                return NotFound();
        }


        [NoCache]
        [HttpGet]
        [Route("imagegallery")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _imageGalleryRepository.GetAllImages();
                if (result != null && result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        [HttpPost]
        [Route("imagegallery")]
        public async Task<IActionResult> AddGallery([FromBody]ImageGallery gallery)
        {
            try
            {

                ImageGallery galDoc = new ImageGallery()
                {
                    Id= ObjectId.GenerateNewId().ToString(),
                    Title = gallery.Title,
                    Comments = gallery.Comments,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    Description = gallery.Description,
                    Path = gallery.Path,
                    UserId=gallery.UserId

                };

                await _imageGalleryRepository.AddImage(galDoc);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest();
            }



        }


    }
}
