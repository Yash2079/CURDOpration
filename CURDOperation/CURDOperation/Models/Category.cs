using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CURDOperation.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
