namespace SkyVisionStore.Domain.Models.Favorite
{
    public class FavoriteInfoModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime AddedAt { get; set; }
    }
}