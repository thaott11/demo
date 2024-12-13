using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(255)]
        public string ImagePath { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0.")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
