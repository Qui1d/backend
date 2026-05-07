namespace SkyVisionStore.Domain.Models.Product
{
    public class ProductInfoModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Platform { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int? Discount { get; set; }

        public string Image { get; set; } = string.Empty;

        public string? RecommendedImage { get; set; }

        public string Region { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string[] Requirements { get; set; } = Array.Empty<string>();

        public bool IsNew { get; set; }

        public bool IsPopular { get; set; }

        public bool IsUpcoming { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}