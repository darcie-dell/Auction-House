namespace Action_house
{
    public class Bid
    {
        /// <summary>
        /// Bid class that creates a Bid on a product, associated by product code. 
        /// </summary>
        public int ProductCode { get; private set; }
        public string BidderName { get; private set; }

        public string BidderEmail { get; private set; }

        public double? BidAmt { get; private set; }

        public string DeliveryOption { get; private set; }

        public ClientAddress DeliveryAddress { get; set; }

        public DateTime? StartDeliveryWindow { get; set; }

        public DateTime? EndDeliveryWindow { get; set; }


        public Bid(Product product, string biddername, string bidderemail, double? bidAmt, string delivopt, ClientAddress clientaddress, DateTime? startdeliverywindow, DateTime? enddeliverywindow)
        {
            this.ProductCode = product.GetHashCode();
            this.BidderName = biddername;
            this.BidderEmail = bidderemail;
            this.BidAmt = bidAmt;
            this.DeliveryOption = delivopt;
            this.DeliveryAddress = clientaddress;
            this.StartDeliveryWindow = startdeliverywindow;
            this.EndDeliveryWindow = enddeliverywindow;


        }

    }
}