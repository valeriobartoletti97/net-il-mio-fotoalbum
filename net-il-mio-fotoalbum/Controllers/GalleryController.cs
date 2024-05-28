using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Models;
using System.Diagnostics;
using net_il_mio_fotoalbum.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
