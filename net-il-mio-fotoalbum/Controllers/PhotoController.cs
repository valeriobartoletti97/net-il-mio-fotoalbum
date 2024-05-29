using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace net_il_mio_fotoalbum.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(ILogger<PhotoController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
           List<Photo> allPhotos = PhotoManager.GetAllPhotos();
           return View(allPhotos);
        }
        public IActionResult GetPhoto(int id)
        {
            Photo photo = PhotoManager.GetPhotoById(id);
            if (photo == null)
            {
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            using (PhotoContext context = new PhotoContext())
            {
                PhotoFormModel model = new PhotoFormModel();
                model.Photo = new Photo();
                model.CreateCategories();
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreateCategories();
                return View("Create", data);
            }
            data.SetImageFileFromFormFile();
            PhotoManager.AddPhoto(data.Photo, data.SelectedCategories);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            Photo photoToEdit = PhotoManager.GetPhotoById(id);
            if (photoToEdit == null)
                return NotFound();
            PhotoFormModel model = new PhotoFormModel(photoToEdit);
            model.CreateCategories();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PhotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreateCategories();
                return View("Update", data);
            }
            var img = data.SetImageFileFromFormFile();
            var modified = PhotoManager.UpdatePhoto(id, data.Photo, data.SelectedCategories,img);
            if (modified)
            {
                // Richiamiamo la action Index affinché vengano mostrate tutte le pizze
                return RedirectToAction("Index");
            }
            else
                return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (PhotoManager.DeletePhoto(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
