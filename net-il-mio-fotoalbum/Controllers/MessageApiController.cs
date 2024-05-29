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
    public class MessageApiController : ControllerBase
    {
      
        [HttpPost]
        public IActionResult SendMessage([FromBody] Message msg)
        {
            MessageManager.SendMessage(msg);
            return Ok($"Il messaggio è stato inviato da ${msg.Email}");
        }
    }
}
