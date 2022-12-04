namespace Action_house
{
    public class Purchase
    {

        /// <summary>
        /// Purchase class creates a purchase with the buyer and purchased item.
        /// </summary>
        /// <value></value>

        public Client Buyer { get; private set; }

        public Product Purchaseditem { get; private set; }

        public Bid WinningBid { get; private set; }


        public Purchase(Client buyer, Product purchaseditem, Bid bid)
        {

            this.Buyer = buyer;
            this.Purchaseditem = purchaseditem;
            this.WinningBid = bid;
        }




    }


}