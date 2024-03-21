using System.ComponentModel.DataAnnotations;

namespace Crud_with_webApi.Model
{
    public class Brand
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? Name { get; set; }
            
        public string? description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

    }
}
