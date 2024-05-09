using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Duzgun daxil edin")]
        public string  Title { get; set; }
        [StringLength(15,ErrorMessage ="Uzunluq maksimum 15 ola biler")]
        public string SubTitle { get; set; }
        public string Description { get; set; }

        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Photofile { get; set; }

    }
}
