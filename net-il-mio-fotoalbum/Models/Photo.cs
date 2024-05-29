using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Il titolo deve essere compreso tra 3 e 30 caratteri")]
        public string Title { get; set; }
        [Required]
        [StringLength(100000, MinimumLength = 5, ErrorMessage = "La descrizione deve essere compresa tra 5 e 10000 caratteri")]

        public string Description { get; set; }
        public string? Image { get; set; }

        public bool IsVisible { get; set; } = true;
        public List<Category>? Categories { get; set; }
        public byte[]? ImageFile { get; set; }
        public string ImgSrc => ImageFile != null ? $"data:image/png;base64,{Convert.ToBase64String(ImageFile)}" : "";

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
