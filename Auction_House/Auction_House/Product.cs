namespace Action_house
{
    public class Product
    {
        /// <summary>
        /// Product class with a product object that is associated to a client email and has a corresponding list of Bids.  
        /// </summary>

        public string Email { get; private set; }

        public string ProductName { get; private set; }

        public string ProductDesc { get; private set; }

        public double? ProductPrice { get; private set; }

        public List<Bid> BidList { get; set; } = new List<Bid>();


        public Product(
            string email,
            string name,
            string desc,
            double? price
        )
        {
            this.Email = email;
            this.ProductName = name;
            this.ProductDesc = desc;
            this.ProductPrice = price;
        }

        /// <summary>
        /// Returns bool based on the existence of a name in a product, used in product search. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public bool SameAs(object obj)
        {
            return ProductName == ((Product)obj).ProductName;
        }

        /// <summary>
        /// Adds a Bid to the Product Bid list. 
        /// </summary>
        /// <param name="NewBid"></param>
        public void PlaceBid(Bid NewBid)
        {
            BidList.Add(NewBid);
        }

        /// <summary>
        /// Method that returns the highest bid in a products bid list, or a placeholder for display.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Bid</returns>
        public Bid HighestBid(Product product)
        {
            if (BidExists(product) == true)
            {
                double? highestBidPrice = product.BidList.Max(bids => bids.BidAmt);
                Bid HighestBid = product.BidList.Find(bids => bids.BidAmt == highestBidPrice);

                return HighestBid;
            }

            else
            {
                Bid placeHolder = new Bid(product, "-", "-", 0, "-", null, null, null);

                return placeHolder;
            }
        }

        /// <summary>
        /// Method that checks if a bid on a product exists.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>bool</returns>
        public bool BidExists(Product product)
        {
            bool exists = false;

            foreach (Bid bid in product.BidList)
            {
                if (product.GetHashCode() == bid.ProductCode)
                {

                    exists = true;
                }
                else
                {
                    exists = false;
                }
            }

            return exists;
        }

        /// <summary>
        /// method that transfers the purchased product over the winners purchased product list, and the bis list for that product is cleared. (not implemented )
        /// </summary>
        /// <param name="winner"></param>
        /// <param name="purchase"></param>
        public void SoldTo(Client winner, Purchase purchase)
        {
            winner.PurchasedProducts.Add(purchase);
            purchase.Purchaseditem.BidList.Clear();
        }

    }
}
