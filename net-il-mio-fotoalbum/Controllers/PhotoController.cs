using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Data;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhotoFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.CreateCategories();
                return View("Create", data);
            }
            PhotoManager.AddPhoto(data.Photo, data.SelectedCategories);
            return RedirectToAction("Index");
        }

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
