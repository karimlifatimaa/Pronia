using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Ad bos ola bilmez!!!")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
