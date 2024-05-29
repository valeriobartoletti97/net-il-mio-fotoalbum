using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Data
{
    public static class PhotoManager
    {
        public static List<Photo> GetAllPhotos()
        {
            using PhotoContext db = new PhotoContext();
            return db.Photos.Include(p => p.Categories).ToList();
        }
        public static List<Photo> GetAllPublicPhotos()
        {
            using PhotoContext db = new PhotoContext();
            return db.Photos.Where(p => p.IsVisible).Include(p => p.Categories).ToList();
        }
        public static List<Category> GetAllCategories()
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.ToList();
        }
        public static Photo GetPhotoById(int id)
        {
            using PhotoContext db = new PhotoContext();
            return db.Photos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();
        }

        public static Category GetCategoryById(int id)
        {
            using PhotoContext db = new PhotoContext();
            return db.Categories.FirstOrDefault(p => p.Id == id);
        }
        public static void AddPhoto(Photo photo, List<string> selectedCategories)
        {
            using PhotoContext db = new PhotoContext();
            photo.Categories = new List<Category>();
            if(selectedCategories != null)
            {
                foreach (var ingredient in selectedCategories)
                {
                    int id = int.Parse(ingredient);
                    var ingredientDB = db.Categories.FirstOrDefault(p => p.Id == id);
                    if (ingredientDB != null)
                    {
                        photo.Categories.Add(ingredientDB);
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

        public static bool DeleteCategory(int id)
        {
            using PhotoContext db = new PhotoContext();
            var category = PhotoManager.GetCategoryById(id);

            if (category == null)
                return false;

            db.Categories.Remove(category);
            db.SaveChanges();

            return true;
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
      
        public static bool UpdatePhoto(int id, Photo photo, List<string> selectedCategories, byte[]? img = null)
        {
            try
            {
                using PhotoContext db = new PhotoContext();
                var photoModificata = db.Photos.Where(p => p.Id == id).Include(p => p.Categories).FirstOrDefault();
                if (photoModificata == null)
                    return false;
                if(img != null)
                {
                    photoModificata.ImageFile = img;
                }
                photoModificata.Title = photo.Title;
                photoModificata.Description = photo.Description;
                photoModificata.IsVisible = photo.IsVisible;
                photoModificata.Categories.Clear();
                if (selectedCategories != null)
                {
                    foreach (var category in selectedCategories)
                    {
                        int categoryId = int.Parse(category);
                        var categoryFromDb = db.Categories.FirstOrDefault(x => x.Id == categoryId);
                        if (categoryFromDb != null)
                            photoModificata.Categories.Add(categoryFromDb);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool DeletePhoto(int id)
        {
            using PhotoContext db = new PhotoContext();
            var pizza = PhotoManager.GetPhotoById(id);

            if (pizza == null)
                return false;

            db.Photos.Remove(pizza);
            db.SaveChanges();

            return true;
        }
        public static void SeedPhotos()
        {
            if (CountAllPhotos() == 0)
            {
                PhotoManager.AddPhoto(new Photo("Bajau Boy", "Un bambino bajau sorride, seduto su una piccola barca di legno, circondato dall'azzurro brillante del mare cristallino.", "bajau_boy.jpg"),new());
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
