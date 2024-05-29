using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using net_il_mio_fotoalbum.Models;
using System.Xml.Linq;
using Message = net_il_mio_fotoalbum.Models.Message;

namespace net_il_mio_fotoalbum.Data
{
    public static class MessageManager
    {
        public static void SendMessage(Message msg)
        {
            using PhotoContext db = new PhotoContext();
            db.Messages.Add(msg);
            db.SaveChanges();
        }
    }
}
