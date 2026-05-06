namespace SkyVisionStore.Domain.Models.Cart
{
    public class AddToCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}