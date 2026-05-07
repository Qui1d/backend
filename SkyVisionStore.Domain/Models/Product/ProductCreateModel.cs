using System.ComponentModel.DataAnnotations;

namespace SkyVisionStore.Domain.Models.Product
{
    public class ProductCreateModel
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int? Discount { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; } = string.Empty;

        [StringLength(500)]
        public string? RecommendedImage { get; set; }

        [Required]
        [StringLength(50)]
        public string Region { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        public string[] Requirements { get; set; } = Array.Empty<string>();

        public bool IsNew { get; set; }

        public bool IsPopular { get; set; }

        public bool IsUpcoming { get; set; }
    }
}