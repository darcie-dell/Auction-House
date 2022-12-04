namespace Action_house
{
    class DisplayL
    {
        /// <summary>
        /// Methods that display lists throughout the program. 
        /// </summary>

        static string ListProductDisplay = "| Item # | Product name | Description | List price | Bidder name | Bidder email | Bid amt |";

        static string ListPurchaseDisplay = "| Item # | Seller Email | Product Name | Description | List Price | Amt paid | Delivery option |";

        /// <summary>
        /// method that Displays Main and Client menu. 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="list"></param>
        /// <param name="prompt"></param>
        /// <typeparam name="T"></typeparam>
        public static void DisplayList<T>(
        string title,
        IList<T> list,
        string? prompt)
        {
            Console.WriteLine(title);
            if (list.Count == 0)
                Console.WriteLine("  None");
            else
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("({0}) {1}", i + 1, list[i].ToString());

            Console.WriteLine(prompt);
        }

        /// <summary>
        /// Method that displays products in the desired format. 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="list"></param>
        /// <param name="error"></param>
        public static void DisplayProductList(
            string title,
            List<Product> list, string error
        )
        {

            Console.WriteLine(title);

            if (list.Count == 0)
                Console.WriteLine(error);
            else
            {
                list = list.OrderBy(product => product.ProductName).ToList();

                int index = 0;

                Console.WriteLine(ListProductDisplay);


                foreach (Product product in list)
                {
                    index++;
                    Console.WriteLine($"| {index} | {product.ProductName} | {product.ProductDesc} | ${product.ProductPrice} | {product.HighestBid(product).BidderName.ToString()} | {product.HighestBid(product).BidderEmail.ToString()} | ${product.HighestBid(product).BidAmt.ToString()} |");

                }
            }


        }

        /// <summary>
        /// Displays purchase list - not working yet - doubles up
        /// </summary>
        /// <param name="title"></param>
        /// <param name="list"></param>
        /// <param name="error"></param>
        public static void DisplayPurchaseList(
            string title,
            List<Purchase> list, string error
        )
        {

            Console.WriteLine(title);

            if (list.Count == 0)
                Console.WriteLine(error);
            else
            {
                list = list.OrderBy(purchase => purchase.Purchaseditem.ProductName).ToList();

                int index = 0;

                Console.WriteLine(ListPurchaseDisplay);


                foreach (Purchase purchase in list)
                {
                    index++;
                    Console.WriteLine($"| {index} | {purchase.Purchaseditem.Email} | {purchase.Purchaseditem.ProductName} | {purchase.Purchaseditem.ProductDesc} | ${purchase.Purchaseditem.ProductPrice} | ${purchase.WinningBid.BidAmt.ToString()} | {purchase.WinningBid.DeliveryOption} |");

                }


            }
        }
    }
}
