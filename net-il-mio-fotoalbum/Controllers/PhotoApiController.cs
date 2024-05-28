using net_il_mio_fotoalbum.Data;
using net_il_mio_fotoalbum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;


namespace net_il_mio_fotoalbum.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoApiController : ControllerBase  //RESTITUISCE TUTTE LE FOTO NEL DB
    {
        [HttpGet]
        public IActionResult GetPhotos(string? title) // .../GetPhoto?name=<<name>>
        {
            if (title != null)
            {
                List<Photo> listaFoto = PhotoManager.GetAllPhotos().Where(p => p.Title.ToLower().Contains(title.ToLower()) && p.IsVisible == true).ToList();
                if (listaFoto.Count > 0)
                    return Ok(listaFoto);
                return NotFound($"Errore la foto {title} non è stata trovata!");
            }
            return Ok(PhotoManager.GetAllPublicPhotos());
        }

        [HttpGet]
        public IActionResult GetPhoto(int id) // ...GetPhoto?id=<<id>>
        {
            Photo photo = PhotoManager.GetPhotoById(id);
            if (photo == null)
            {
                return NotFound("ERRORE! LA FOTO NON E' STATA TROVATA");
            }
            return Ok(photo);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreatePhoto([FromBody] Photo data)
        {
            PhotoManager.AddPhoto(data, null);
            return Ok($"La foto \"{data.Title}\" è stata creata!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdatePhoto(int id, [FromBody] Photo data) // ...UpdatePhoto?id=<<id>>
        {
            var FotoDaModificare = PhotoManager.GetPhotoById(id);
            if (FotoDaModificare == null)
                return NotFound($"Errore la foto {data.Title} non è stata trovata!");
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete]
        public IActionResult DeletePhoto(int id) // ...UpdatePhoto?id=<<id>>
        {
            Photo foto = PhotoManager.GetPhotoById(id);
            if (foto == null)
            {
                return NotFound("ERRORE! LA FOTO NON E' STATA TROVATA");
            }

            var isDeleted = PhotoManager.DeletePhoto(id);
            if (!isDeleted)
                return NotFound("Non è stato possibile eliminare la foto");
            return Ok($"La foto {foto.Title} è stata eliminata con successo");
        }
    }


}