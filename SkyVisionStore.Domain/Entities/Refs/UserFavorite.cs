using SkyVisionStore.Domain.Entities.Product;
using SkyVisionStore.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyVisionStore.Domain.Entities.Refs
{
    public class UserFavorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public DateTime AddedAt { get; set; }

        public User User { get; set; }

        public Product Product { get; set; }
    }
}
