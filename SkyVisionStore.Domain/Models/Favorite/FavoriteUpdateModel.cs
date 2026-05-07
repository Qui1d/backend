using System.ComponentModel.DataAnnotations;

namespace SkyVisionStore.Domain.Models.Favorite
{
    public class FavoriteUpdateModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}