namespace Mango.web.Models
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        // to make some calculation in  only mango.web
        public double OrdarTotal { get; set; }
    }
}
