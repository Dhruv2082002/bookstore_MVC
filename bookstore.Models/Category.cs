using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]

        [MaxLength(100)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Range(0, 1000)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
