namespace Action_house
{
    class Database
    {
        /// <summary>
        /// Database class that creates a filesystem instance and holds a current client token, aimed to implement this as an interface between the user and the filesystem. 
        /// </summary>
        public Client? CurrentClient { get; set; }

        FileSystem
            FileSystem = new FileSystem();

        /// <summary>
        /// Registers a client into the system and save credentials for later user.
        /// </summary>
        public void Register()
        {
            Console.WriteLine("");
            Console.WriteLine("Registration");
            Console.WriteLine("----------");
            Console.WriteLine("");

            string clientName = Validation.GetLetters("Please enter your name", "name");

            while (String.IsNullOrWhiteSpace(clientName)
            )
            {
                Validation.InvalidError("name");

                clientName = Validation.GetLetters("Please enter your name", "name");
            }

            Console.WriteLine("");
            string clientEmail = Validation.GetEmail("Please enter your email address");

            bool EmailInUse = FileSystem.ContainsEmail(clientEmail);

            if (EmailInUse == true)
            {
                Validation.Error("Email already exists");
            }

            while (String.IsNullOrWhiteSpace(clientEmail) || EmailInUse == true)
            {
                Validation.InvalidError("email");

                clientEmail = Validation.GetEmail("Please enter your email address");

                EmailInUse = FileSystem.ContainsEmail(clientEmail);
            }

            string clientpwd =
                Validation
                    .GetPwd("\nPlease choose a Pwd \n* At least 8 characters\n* No white space characters\n* At least one upper-case letter\n* At least one lower-case letter\n* At least one digit\n* At least one special character");
            while (String.IsNullOrWhiteSpace(clientpwd))
            {
                Validation.InvalidError("password");
                clientpwd = Validation.GetPwd("\nPlease choose a Pwd \n* At least 8 characters\n* No white space characters\n* At least one upper-case letter\n* At least one lower-case letter\n* At least one digit\n* At least one special character");
            }

            Client NewClient =
                new Client(clientName, clientEmail, clientpwd);

            FileSystem.SaveClient(NewClient);

            Console.WriteLine("");
            Console
                .WriteLine($"Client {clientName}({clientEmail}) has successfully registered at the Auction House.");
            Console.WriteLine("");
        }

        /// <summary>
        /// Checks a users input against stored credentials and sets the user as the current client accordingly. 
        /// </summary>
        public void Login()
        {

            Console.WriteLine("");
            Console.WriteLine("Sign In");
            Console.WriteLine("-------");
            Console.WriteLine("");

            while (CurrentClient == null)
            {

                string clientEmailLogin =
                    Validation.GetEmail("Please enter your email address");
                while (String.IsNullOrWhiteSpace(clientEmailLogin))
                {
                    Validation.InvalidError("email");
                    clientEmailLogin = Validation.GetEmail("Please enter your email address");
                }
                Console.WriteLine("");
                string clientPwdLogin =
                    Validation.GetPwd("Please enter your password");
                while (String.IsNullOrWhiteSpace(clientPwdLogin))
                {
                    Validation.InvalidError("password");
                    clientPwdLogin =
                        Validation.GetPwd("Please enter your password");
                }

                bool ClientCredsExists = FileSystem.FindClientCreds(clientEmailLogin, clientPwdLogin);

                if (ClientCredsExists == true)
                {
                    CurrentClient = FileSystem.Find(clientEmailLogin);
                }
                else
                    Validation.InvalidError("registered client");
            }

            bool AddressExists = FileSystem.ContainsEmailAddress(CurrentClient.Email);

            if (!AddressExists == true)
            {
                Console.WriteLine("");
                Console.WriteLine($"Personal Details for {CurrentClient.ToString()}");

                RegisterAddress();
            }
        }

        /// <summary>
        /// Clears the logged in account that resets the menu system in MainMenu class. 
        /// </summary>
        public void Logout()
        {
            CurrentClient = null;
        }

        /// <summary>
        /// Exits the program gracefully :)
        /// </summary>
        public void Exit()
        {
            Console.WriteLine("+--------------------------------------------------+\n| Good bye, thank you for using the Auction House! |\n+--------------------------------------------------+\n");
            Environment.Exit(0);
        }

        /// <summary>
        /// Calls the address registration dialogue and saves to the filesystem. 
        /// </summary>
        public void RegisterAddress()
        {
            //Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------\nPlease provide your home address.");
            Console.WriteLine("");
            ClientAddress NewAddress = GetAddress("Address has been updated to");
            FileSystem.SaveAddress(NewAddress);
        }

        /// <summary>
        /// Prompts the user to register their address. 
        /// </summary>
        /// <param name="AddressDeclaration"></param>
        /// <returns>Client address object </returns>
        public ClientAddress GetAddress(string AddressDeclaration)
        {
            string stringunitnum = Validation.GetUnitNum("Unit number (0 = none)", "Unit number");
            while (String.IsNullOrWhiteSpace(stringunitnum))
            {
                Validation.InvalidError("non negative integer");
                stringunitnum = Validation.GetUnitNum("Unit number (0 = none)", "Unit number");
            }
            int unitnum;
            while (!int.TryParse(stringunitnum, out unitnum) && unitnum < 0)
            {
                Validation.InvalidError("non negative integer");
                Validation.GetInput("Unit number (0 = none)");
            }

            Console.WriteLine("");
            string stringstreetnum = Validation.GetInteger("Street number", "Street number");
            while (String.IsNullOrWhiteSpace(stringstreetnum))
            {
                Validation.InvalidError("non negative integer");
                stringstreetnum = Validation.GetInteger("Street number", "Street number");
            }
            int streetnum;
            while (!int.TryParse(stringstreetnum, out streetnum))
            {
                Validation.InvalidError("non negative integer");
                stringunitnum = Validation.GetInteger("Street number", "Street number");
            }
            Console.WriteLine("");
            string streetName =
                Validation.GetLetters("Street name");
            while (String.IsNullOrWhiteSpace(streetName)
            )
            {
                Validation.InvalidError("street name");
                streetName =
                    Validation.GetLetters("Street name");
            }
            Console.WriteLine("");
            string streetsuff = Validation.GetLetters("Street suffix");
            while (String.IsNullOrWhiteSpace(streetsuff)
            )
            {
                Validation.InvalidError("street suffix");
                streetsuff = Validation.GetLetters("Street suffix");
            }
            Console.WriteLine("");
            string city = Validation.GetLetters("City");
            while (String.IsNullOrWhiteSpace(city)
            )
            {
                Validation.InvalidError("city");
                city = Validation.GetLetters("City");
            }
            Console.WriteLine("");
            string state =
                Validation
                    .GetState("State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA)");
            while (String.IsNullOrWhiteSpace(state)
            )
            {
                Validation.InvalidError("state");
                state =
                    Validation
                        .GetState("State (ACT, NSW, NT, QLD, SA, TAS, VIC, WA)");
            }
            Console.WriteLine("");
            string postcode = Validation.GetPostcode("Postcode (1000 .. 9999)");
            while (String.IsNullOrWhiteSpace(postcode))
            {
                Validation.InvalidError("postcode");
                postcode = Validation.GetPostcode("Postcode (1000 .. 9999)");
            }

            ClientAddress NewAddress = new ClientAddress(CurrentClient.Email, unitnum, streetnum, streetsuff, city, postcode, state);

            Console.WriteLine("\n");
            Console.WriteLine($"{AddressDeclaration} {unitnum}/{streetnum} {streetName} {streetsuff}, {city} {state} {postcode}");

            return NewAddress;
        }


        /// <summary>
        /// Creates a Product object and saves it to the filesystem. 
        /// </summary>
        public void AdvProduct()
        {
            Console.WriteLine("");
            Console.WriteLine($"Product Advertisement for {CurrentClient.ToString()}");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("");

            string productName = Validation.GetLetters("Product name");
            while (String.IsNullOrWhiteSpace(productName)
            )
            {
                Validation.InvalidError("product name");
                productName = Validation.GetLetters("Product name");
            }
            Console.WriteLine("");
            string productDesc = Validation.GetProductDesc("Product Description", productName);
            while (String.IsNullOrWhiteSpace(productName)
            )
            {
                Validation.InvalidError("product description");
                productName = Validation.GetProductDesc("Product Description", productName);
            }
            Console.WriteLine("");
            double productPrice = Validation.GetPrice("Product price ($d.cc)");
            while (String.IsNullOrWhiteSpace(productName)
            )
            {
                Validation.InvalidError("currency value");
                productPrice = Validation.GetPrice("Product price ($d.cc)");
            }

            int Count = FileSystem.AllProducts().Count;

            int productNum = +Count;

            Product NewProduct = new Product(CurrentClient.Email, productName, productDesc, productPrice);
            FileSystem.SaveProduct(NewProduct);
            Console.WriteLine("");
            Console.WriteLine($"Successfully added product {productName}, {productDesc}, ${productPrice}");
        }

        /// <summary>
        /// Prompts the user to search for a product, and returns a list of products based on the search and calls place bid dialogue using the search results. 
        /// </summary>
        public void SearchProducts()
        {
            Console.WriteLine("");
            Console.WriteLine($"Product search for {CurrentClient.ToString()}");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("");

            string SearchPhrase = Validation.GetInput("Please supply a search phrase (ALL to see all products)");

            while (String.IsNullOrWhiteSpace(SearchPhrase)
            )
            {
                Validation.InvalidError("search phrase");
                SearchPhrase = Validation.GetInput("Please supply a search phrase (ALL to see all products)");
            }

            List<Product> FoundProducts;

            List<Product> FoundProductsAll;

            if (SearchPhrase.ToUpper() == "ALL")
            {
                FoundProducts = FileSystem.OtherProducts(CurrentClient);
            }

            else
            {
                FoundProductsAll = FileSystem.OtherProducts(CurrentClient);

                FoundProducts = FileSystem.GetMatches(SearchPhrase, FoundProductsAll);

                if (FoundProducts.Count() == 0)
                {
                    FoundProducts = FileSystem.GetMatchesD(SearchPhrase, FoundProductsAll);
                }
            }

            Console.WriteLine("");
            DisplayL.DisplayProductList($"Search Results\n----------------------", FoundProducts, "No Products Found");
            Console.WriteLine("");

            if (FoundProducts.Count != 0)
            {
                string YesNo = Validation.GetYesNo("Would you like to place a bid on any of these items (yes or no)?");
                while (String.IsNullOrWhiteSpace(YesNo)
                )
                {
                    Validation.InvalidError("input (yes or no)");
                    YesNo = Validation.GetYesNo("Would you like to place a bid on any of these items (yes or no)?");
                }

                if (YesNo.ToUpper() == "YES")
                {
                    //here

                    PlaceBidDialouge(FoundProducts);
                }

                else { }
            }

        }

        /// <summary>
        /// Takes the passed List of search results and prompts the user to place a bid accordinly, creates a bif object and saves this to the filesystem. 
        /// </summary>
        /// <param name="FoundList"></param>
        public void PlaceBidDialouge(List<Product> FoundList)
        {

            bool noProducts = !FoundList.Any();

            List<Product> FoundListOrdered = FoundList.OrderBy(product => product.ProductName).ToList();

            int Count = FoundListOrdered.Count();

            if (!noProducts)
            {
                Console.WriteLine("");
                int BidNumOfProduct = Validation.getOption($"Please enter a non-negative integer between 1 and {Count}", 1, Count);

                string BidNumOfProductString = BidNumOfProduct.ToString();

                bool EmptyInput = String.IsNullOrWhiteSpace(BidNumOfProductString);

                if (EmptyInput) //Error handling
                {
                    BidNumOfProduct = Validation.getOption($"Invalid input: Please enter a non-negative integer between 1 and {Count}", 1, Count);
                }

                Product CurrentProduct = FileSystem.GetProduct(FoundListOrdered, BidNumOfProduct + 1);

                Console.WriteLine("");
                Console.WriteLine($"Bidding for {CurrentProduct.ProductName} (regular price ${CurrentProduct.ProductPrice}), current highest bid ${CurrentProduct.HighestBid(CurrentProduct).BidAmt.ToString()})");

                double bidPrice = Validation.GetPrice("How much do you bid?");

                while (bidPrice <= CurrentProduct.HighestBid(CurrentProduct).BidAmt)
                {
                    Validation.InvalidError($"Bid must be higher than ${CurrentProduct.HighestBid(CurrentProduct).BidAmt.ToString()}");
                    bidPrice = Validation.GetPrice("How much do you bid?");
                }

                Console.WriteLine("");
                Console.WriteLine($"Your bid of ${bidPrice} for {CurrentProduct.ProductName} is placed.");
                Console.WriteLine("");
                Console.WriteLine("Delivery Instructions");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("(1) Click and collect");
                Console.WriteLine("(2) Home Delivery");
                Console.WriteLine("");
                int DeliveryOpts = Validation.getOption("Please enter a non-negative integer between 1 and 2", 1, 2) + 1;

                bool EmptyInput2 = String.IsNullOrWhiteSpace(DeliveryOpts.ToString());

                if (EmptyInput2) //Error handling
                {
                    BidNumOfProduct = Validation.getOption("Please enter a non-negative integer between 1 and 2", 1, 2);
                }

                string deliveryOption;

                if (DeliveryOpts == 1)
                {
                    deliveryOption = "Click and collect";

                    Console.WriteLine("");
                    DateTime StartWindow = Validation.GetDateTime("Delivery window start (dd/mm/yyyy hh:mm)");

                    DateTime now = DateTime.Now;

                    bool validTime = false;

                    if (DateTime.Compare(StartWindow, now.AddHours(1)) >= 0)
                    {
                        validTime = true;
                    }
                    if (StartWindow == null || validTime == false)
                    {
                        Console.WriteLine("Invalid Input: Delivery window start must be at least one hour in the future.");
                        Console.WriteLine("Invalid Input: Please enter a valid data and time.");
                    }
                    Console.WriteLine("");
                    DateTime EndWindow = Validation.GetDateTime("Delivery window end (dd/mm/yyyy hh:mm)");

                    if (DateTime.Compare(StartWindow.AddHours(1), EndWindow) >= 0)
                    {
                        validTime = true;
                    }
                    if (EndWindow == null || validTime == false)
                    {
                        Console.WriteLine("Invalid Input: Delivery window end must be at least one hour greater than the start window.");
                        Console.WriteLine("Invalid Input: Please enter a valid data and time.");
                    }

                    ClientAddress NullDeliveryAddress = new ClientAddress("-", 0, 0, "-", "-", "-", "-");

                    Bid NewBid = new Bid(CurrentProduct, CurrentClient.Name, CurrentClient.Email, bidPrice, deliveryOption, NullDeliveryAddress, StartWindow, EndWindow);
                    CurrentProduct.PlaceBid(NewBid);
                }
                if (DeliveryOpts == 2)
                {
                    deliveryOption = "Home Delivery";
                    Console.WriteLine("");
                    Console.WriteLine("Please provide your delivery address.");
                    Console.WriteLine("");
                    ClientAddress DeliveryAddress = GetAddress("Thank you for your bid. If successful, the item will be provided via delivery to");


                    //placeholder for a date time because null didn't work 
                    DateTime NullDt = new DateTime(2022, 01, 01, 00, 00, 00);
                    Bid NewBid = new Bid(CurrentProduct, CurrentClient.Name, CurrentClient.Email, bidPrice, deliveryOption, DeliveryAddress, NullDt, NullDt);
                    CurrentProduct.PlaceBid(NewBid);
                }

                FileSystem.SaveClientsBids();
            }

            else Console.WriteLine("No products to bid on");
        }

        /// <summary>
        /// Displays a list of the products advertised by the current logged in client. 
        /// </summary>
        public void ViewMyProducts()
        {
            List<Product> myProducts = FileSystem.CurrentLoggedInProducts(CurrentClient);
            DisplayL.DisplayProductList($"Product List for {CurrentClient.ToString()}", myProducts, "You have no advertised products at the moment.");

        }

        /// <summary>
        /// generates a list of the current logged in clients products bids and passes this list to begin the sell product dialogue.  
        /// </summary>
        public void ViewBids()
        {
            List<Product> BidsOnProduct = FileSystem.GetBids(CurrentClient);

            if (BidsOnProduct.Count == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("There are no bids on your products");
            }

            else
            {
                DisplayL.DisplayProductList($"List Product Bids for {CurrentClient.ToString()} \n ------------------------------------------------------------------------", BidsOnProduct, "No Bids Found");
                Console.WriteLine("");
                string YesNo = Validation.GetYesNo("Would you like to sell something (yes or no)?");

                while (String.IsNullOrWhiteSpace(YesNo)
                )
                {
                    Console
                        .WriteLine("Invalid Input: The supplied value is not a valid input");
                    YesNo = Validation.GetYesNo("Would you like to sell something (yes or no)?");
                }

                if (YesNo.ToUpper() == "YES")
                {
                    SellProduct(BidsOnProduct.OrderBy(product => product.ProductName).ToList());
                }
            }
        }

        /// <summary>
        /// Takes a list of the current logged in users products with bids on them and passes the product ownership to the auction winner via the sell product method, soon :)
        /// </summary>
        /// <param name="BidsOnProduct"></param>
        public void SellProduct(List<Product> BidsOnProduct)
        {

            int Count = BidsOnProduct.Count();

            string BidNumOfProduct = Validation.GetInput($"Please enter a non-negative integer between 1 and {Count}");

            bool EmptyInput = String.IsNullOrWhiteSpace(BidNumOfProduct);

            if (!EmptyInput) //Error handling
            {
                while (!Int32.TryParse(BidNumOfProduct, out int BidNumOfProductsInt))
                {
                    BidNumOfProduct = Validation.GetInput($"Invalid input: Please enter a non-negative integer between 1 and {Count}");
                }
            }

            Int32.TryParse(BidNumOfProduct, out int BidNumOfProductInt);

            //return product that the client selects to sell
            Product ProductToSell = FileSystem.GetProduct(BidsOnProduct, BidNumOfProductInt);

            //get the highest bid 
            Bid WinningBid = ProductToSell.HighestBid(ProductToSell);

            string WinnerEmail = ProductToSell.HighestBid(ProductToSell).BidderEmail.ToString();

            Client Winner = FileSystem.HighestBidClient(ProductToSell, WinnerEmail);

            // get here because selling clear list 
            string AuctionAmount = ProductToSell.HighestBid(ProductToSell).BidAmt.ToString();

            Purchase NewPurchase = new Purchase(Winner, ProductToSell, WinningBid);

            ProductToSell.SoldTo(Winner, NewPurchase);

            FileSystem.SavePurchase(Winner, NewPurchase);

            Console.WriteLine($"You have sold {ProductToSell.ProductName} to {Winner.Name} for ${AuctionAmount}.");
        }

        /// <summary>
        /// Gets a list of the current users purchases. 
        /// </summary>
        /// 
        /// Purchase not completed 
        public void ViewMyPurchases()
        {
            //get purchases from list where email = current client email 
            Console.WriteLine("");
            DisplayL.DisplayPurchaseList($"Purchased Items for {CurrentClient.ToString()} \n ----------------------------------------------------------------------------", CurrentClient.PurchasedProducts, "No Purchased products Found");
        }


    }
}
