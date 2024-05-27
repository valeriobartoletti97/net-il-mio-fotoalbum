using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public static class PhotoManager
    {
        public static void AddPhoto(Photo photo, List<string>? selectedCategories = null)
        {
            using PhotoContext db = new PhotoContext();
            photo.Categories = new List<Category>();
            if (selectedCategories != null)
            {
                foreach (var category in selectedCategories)
                {
                    int id = int.Parse(category);
                    var categoryDB = db.Categories.FirstOrDefault(p => p.Id == id);
                    if (categoryDB != null)
                    {
                        photo.Categories.Add(categoryDB);
                    }
                }
            }
            db.Photos.Add(photo);
            db.SaveChanges();
        }
        public static void AddCategory(string name)
        {
            using PhotoContext db = new PhotoContext();
            Category newCategory = new Category(name);
            db.Categories.Add(newCategory);
            db.SaveChanges();
        }

        public static int CountAllPhotos() 
        {
            using PhotoContext db = new PhotoContext();
            return db.Photos.Count();
        }
        public static int CountAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.Count();
        }
      
        public static void SeedPhotos()
        {
            if (CountAllPhotos() == 0)
            {
                PhotoManager.AddPhoto(new Photo("Bajau Boy", "Un bambino bajau sorride, seduto su una piccola barca di legno, circondato dall'azzurro brillante del mare cristallino.", "bajau_boy.jpg"), new());
                PhotoManager.AddPhoto(new Photo("Rock Splash", "Un imponente scoglio emerge dal mare, mentre onde vigorose si infrangono contro di esso, spruzzando acqua scintillante tutt'intorno.", "brandung_im_meer_an_felsen.jpg"), new());
                PhotoManager.AddPhoto(new Photo("Cosmic Micro", "Una miriade di galassie, catturate al microscopio, si dispongono in intricate formazioni, brillando come gioielli nel vasto universo.", "das_pferd_am_himmel.jpg"), new());
                PhotoManager.AddPhoto(new Photo("Tiger Love", "Due tigri si coccolano affettuosamente, i loro corpi striati intrecciati, esprimendo un legame profondo e intimo nella natura selvaggia.", "feline_family.jpg"), new());
                PhotoManager.AddPhoto(new Photo("Muddy Match", "Bambini giocano a calcio nel fango, ridendo e scivolando, mentre il pallone schizza terra e acqua in un'esplosione di gioia.", "playing_football.jpg"), new());
                PhotoManager.AddPhoto(new Photo("Sandstorm Duo", "Un uomo e un cammello avanzano insieme, avvolti in una tempesta di sabbia, affrontando il deserto con determinazione e resilienza.", "sand-storm.jpg"), new());
                PhotoManager.AddPhoto(new Photo("Golden Reflection", "Una solitaria spiga di grano si specchia nel lago tranquillo, creando un'immagine dorata e serena nell'acqua calma.", "schilfrohr_spiegelung_im_blauem_wasser.jpg"), new());
            }
        }
        public static void SeedCategories()
        {
            if (CountAllCategories() == 0)
            {
                PhotoManager.AddCategory("Ritratto");
                PhotoManager.AddCategory("Moda");
                PhotoManager.AddCategory("Paesaggio");
                PhotoManager.AddCategory("Sport");
                PhotoManager.AddCategory("Animali");
                PhotoManager.AddCategory("Astronomia");
                PhotoManager.AddCategory("Natura");
            }
        }
    }
}
