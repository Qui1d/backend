namespace SkyVisionStore.Domain.Entities.User
{
    public class UserFavorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}