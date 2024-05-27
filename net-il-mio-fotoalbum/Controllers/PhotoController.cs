using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Data;

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
    }
}
