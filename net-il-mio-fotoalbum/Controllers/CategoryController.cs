using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            List<Category> allCategories = PhotoManager.GetAllCategories();
            return View(allCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }
            PhotoManager.AddCategory(data.Name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            if (PhotoManager.DeleteCategory(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
