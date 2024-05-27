using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

        public bool IsVisible { get; set; } = true;
        public List<Category>? Categories { get; set; }

        public Photo()
        {

        }
        public Photo(string title, string description, string image)
        {
            this.Title = title;
            this.Description = description;
            this.Image = image;
        }
    }
}
