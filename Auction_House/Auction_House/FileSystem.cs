using Action_house;

class FileSystem
{
    /// <summary>
    /// FileSystem class handles add the data transfer between the files, classes and objects. I intended for this class to act as a "Back-End" to handle 
    /// the auction house data. I am aware this is bad design, I intended to separate these out but ran out of time to correctly implement. 
    /// </summary>

    /// <summary>Name of save-data files.</summary>
    private string ClientData = "ClientData.txt";

    private string AddressData = "AddressData.txt";

    private string ProductData = "ProductsData.txt";

    private string BidData = "BidsData.txt";

    private string PurchaseData = "PurchasedItems.txt";

    /// <summary>Lists that store the active data within the instance. </summary>
    private List<Client> ClientList { get; set; } = new List<Client>();

    private List<ClientAddress> AddressList { get; set; } = new List<ClientAddress>();

    private List<Product> ProductList { get; set; } = new List<Product>();

    /// <summary>
    /// Loading data from files on filesystem creation.
    /// </summary>
    public FileSystem()
    {
        Load();
    }

    private void Load()
    {
        LoadAllClient();
        LoadAllAddress();
        LoadAllProducts();
        LoadAllBids();
        LoadAllPurchases();
    }

    /// <summary>Saves data to a text file, able to be parsed by Load.</summary>
    /// <summary>
    /// I am also aware the multiple save and load methods are not good practice but i wanted to be very careful with the data and how it was transferred 
    /// as i have multiple files and streams. 
    /// </summary>
    public void SaveClientsList()
    {
        using (StreamWriter w = new StreamWriter(ClientData))
        {
            foreach (Client client in ClientList)
            {
                w.WriteLine(client.Name);
                w.WriteLine(client.Email);
                w.WriteLine(client.Pwd);
            }

            w.Close();
        }
    }

    /// <summary>
    /// Adds Client to active list and saves client to client file.
    /// </summary>
    /// <param name="client"></param>

    public void SaveClient(Client client)
    {
        ClientList.Add(client);
        SaveClientsList();
    }

    /// <summary>
    /// Finds Client by supplied email.
    /// </summary>
    /// <param name="clientEmailLogin"></param>
    /// <returns>Client with same email as supplied. </returns>
    public Client Find(string clientEmailLogin)
    {

        Client Found = ClientList.Find(Client => Client.SameAs(new Client(null, clientEmailLogin, null)));

        return Found;
    }

    /// <summary>
    /// Finds clients stored address by supplied email.
    /// </summary>
    /// <param name="clientEmail"></param>
    /// <returns>client address that corresponds to email. </returns>
    public ClientAddress FindAddress(string clientEmail)
    {

        ClientAddress Found = AddressList.Find(ClientAddress => ClientAddress.SameAs(new ClientAddress(clientEmail, null, null, null, null, null, null)));


        return Found;
    }

    /// <summary>
    /// Finds product by supplied product name.
    /// </summary>
    /// <param name="productName"></param>
    /// <returns>Product that corresponds to product name.</returns>
    public Product FindProduct(string productName)
    {

        Product Found = ProductList.Find(Product => Product.SameAs(new Product(null, productName, null, null)));

        return Found;
    }

    /// <summary>
    /// Finds product by supplied hash.
    /// </summary>
    /// <param name="Hash"></param>
    /// <returns>Product with the same hash as supplied. </returns>
    public Product FindProductByHash(string Hash)
    {
        Product FoundProduct = new Product(null, null, null, null);

        foreach (Product product in ProductList)
        {
            string CompareHash = product.GetHashCode().ToString();

            if (string.Compare(CompareHash, Hash) == 0)
            {
                FoundProduct = product;
            }
        }
        return FoundProduct;
    }

    /// <summary>
    /// Finds if a Client is already registered in the system or not.
    /// </summary>
    /// <param name="clientEmailLogin"></param>
    /// <param name="clientPwdLogin"></param>
    /// <returns>Bool depending on existance.</returns>
    public bool FindClientCreds(string clientEmailLogin, string clientPwdLogin)
    {

        Client Found = ClientList.Find(Client => Client.SameAs(new Client(null, clientEmailLogin, clientPwdLogin)));

        if (Found != null)
        {
            return true;
        }

        else return false;
    }

    /// <summary>
    /// Gets the Client from the BidList that has supplied the highest bid on a product .
    /// </summary>
    /// <param name="product"></param>
    /// <param name="HighestBidderEmail"></param>
    /// <returns>The Client that has made the highest bid on a product.</returns>
    public Client HighestBidClient(Product product, string HighestBidderEmail)
    {
        if (product.BidExists(product))
        {
            double? highestBidPrice = product.BidList.Max(bids => bids.BidAmt);

            Bid HighestBid = product.BidList.Find(bids => bids.BidAmt == highestBidPrice);

            //finding client that has the highest bid by email
            Client Winner = ClientList.Find(Client => Client.SameAs(new Client(null, HighestBidderEmail, null)));

            return Winner;
        }

        //fix this here 
        else
        {
            //was lazy and did this. 
            Client placeHolder = new Client(null, null, null);

            return placeHolder;
        }
    }


    /// <summary>
    /// Saves Clients address, able to be parsed by Load.
    /// </summary>
    public void SaveClientsAddress()
    {
        using (StreamWriter w = new StreamWriter(AddressData))
        {
            foreach (ClientAddress address in AddressList)
            {
                w.WriteLine(address.Email);
                w.WriteLine(address.UnitNumber);
                w.WriteLine(address.StreetNumber);
                w.WriteLine(address.StreetSuffix);
                w.WriteLine(address.City);
                w.WriteLine(address.Postcode);
                w.WriteLine(address.State);
            }
            w.Close();
        }
    }

    /// <summary>
    /// Saves Client address and adds to active list.
    /// </summary>
    /// <param name="clientAddress"></param>
    public void SaveAddress(ClientAddress clientAddress)
    {
        AddressList.Add(clientAddress);
        SaveClientsAddress();
    }


    /// <summary>
    /// Opens stream reader for client data to be read and verify identity is valid.
    /// </summary>
    public void LoadAllClient()
    {
        if (File.Exists(ClientData))
        {
            using (StreamReader r = new StreamReader(ClientData))
            {
                while (true)
                {
                    string Ident = r.ReadLine();

                    if (Ident == null)
                        break;
                    else
                    {
                        LoadClient(Ident, r);
                    }

                }
            }
        }
    }

    /// <summary>
    /// Opens stream reader for client addresses to be read and verifys indentity is valid.
    /// </summary>
    public void LoadAllAddress()
    {
        if (File.Exists(AddressData))
        {
            using (StreamReader r = new StreamReader(AddressData))
            {
                while (true)
                {
                    string Ident = r.ReadLine();

                    if (Ident == null)
                        break;
                    else
                    {
                        LoadAddress(Ident, r);
                    }

                }
            }
        }
    }

    /// <summary>
    /// Parses Client from text reader.
    /// </summary>
    /// <param name="name_"></param>
    /// <param name="r"></param>

    private void LoadClient(string name_, TextReader r)
    {
        string email_ = r.ReadLine();
        string pwd_ = r.ReadLine();


        //addional valdation when file reading
        if (
            Validation.GetInputBool(name_) &&
            Validation.GetInputBool(email_) &&
            Validation.GetInputBool(pwd_)
        )
        {
            ClientList.Add(new Client(name_, email_, pwd_));
        }
        else
        {
            Console.Error.WriteLine("File Read Failed");
        }
    }

    /// <summary>
    /// Checks if Client list contains the supplied email.
    /// </summary>
    /// <param name="compareValue"></param>
    /// <returns>True or False respectivly.</returns>
    public bool ContainsEmail(string compareValue)
    {
        if (ClientList.Any(Client => Client.Email == compareValue) == true)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Checks if Client has registered an address.
    /// </summary>
    /// <param name="compareValue"></param>
    /// <returns>True or false respectivly.</returns>
    public bool ContainsEmailAddress(string compareValue)
    {
        if (AddressList.Any(ClientAddress => ClientAddress.Email == compareValue) == true)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Checks if a password exists.
    /// </summary>
    /// <param name="compareValue"></param>
    /// <returns>True or false respectivly.</returns>
    public bool ContainsPwd(string compareValue)
    {
        if (ClientList.Any(Client => Client.Pwd == compareValue) == true)
        {
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Parses addresses from text reader.
    /// </summary>
    /// <param name="email_"></param>
    /// <param name="r"></param>
    private void LoadAddress(string email_, TextReader r)
    {
        string unitnum_ = r.ReadLine();
        string streetnumber = r.ReadLine();
        string streetsuffix = r.ReadLine();
        string city = r.ReadLine();
        string postcode = r.ReadLine();
        string state = r.ReadLine();

        if (
            Validation.GetInputBool(streetnumber) &&
            Validation.GetInputBool(streetsuffix) &&
            Validation.GetInputBool(city) &&
            Validation.GetInputBool(postcode) &&
            Validation.GetInputBool(state)
        )
        {

            int unitnumInt = int.Parse(unitnum_);

            int streetnumInt = int.Parse(unitnum_);


            AddressList.Add(new ClientAddress(email_, unitnumInt, streetnumInt, streetsuffix, city, postcode, state));
        }
        else
        {
            Console.Error.WriteLine("File Read Failed");
        }
    }

    /// <summary>
    /// Opens stream to read product data from file.
    /// </summary>
    public void LoadAllProducts()
    {
        if (File.Exists(ProductData))
        {
            using (StreamReader r = new StreamReader(ProductData))
            {
                while (true)
                {
                    string Ident = r.ReadLine();

                    if (Ident == null)
                        break;
                    else
                    {
                        LoadProduct(Ident, r);
                    }

                }
            }
        }
    }

    /// <summary>
    /// Parses product data from stream reader.
    /// </summary>
    /// <param name="email_"></param>
    /// <param name="r"></param>
    private void LoadProduct(string email_, TextReader r)
    {
        //address part
        string productName = r.ReadLine();
        string productDesc = r.ReadLine();
        string productPrice_ = r.ReadLine();



        //addional valdation when file reading
        if (
            Validation.GetInputBool(productName) &&
            Validation.GetInputBool(productDesc)
        //need to add double validation here 

        )
        {

            double productPrice = double.Parse(productPrice_);

            ProductList.Add(new Product(email_, productName, productDesc, productPrice));
        }
        else
        {
            Console.Error.WriteLine("File Read Failed");
        }
    }

    /// <summary>
    /// Writes active product data to file.
    /// </summary>
    public void SaveClientsProducts()
    {
        using (StreamWriter w = new StreamWriter(ProductData))
        {
            foreach (Product product in ProductList)
            {
                w.WriteLine(product.Email);
                w.WriteLine(product.ProductName);
                w.WriteLine(product.ProductDesc);
                w.WriteLine(product.ProductPrice);
            }
            w.Close();
        }
    }


    /// <summary>
    /// Saves product data to file and adds to active list.
    /// </summary>
    /// <param name="product"></param>
    public void SaveProduct(Product product)
    {
        ProductList.Add(product);
        SaveClientsProducts();
    }

    /// <summary>
    /// Gets the products that the current logged in user has advertised.
    /// </summary>
    /// <param name="CurrentClient"></param>
    /// <returns>List of products.</returns>
    public List<Product> CurrentLoggedInProducts(Client CurrentClient)
    {
        return ProductList.Where(product => (product.Email).Equals(CurrentClient.Email)).ToList();
    }

    /// <summary>
    /// Gets the products the current logged in user hasnt advertised.
    /// </summary>
    /// <param name="CurrentClient"></param>
    /// <returns>List of Products.</returns>
    public List<Product> OtherProducts(Client CurrentClient)
    {
        return ProductList.Where(product => (!(product.Email).Equals(CurrentClient.Email))).ToList();
    }

    /// <summary>
    /// Gets all currently advertised products.
    /// </summary>
    /// <returns>List of Products.</returns>
    public List<Product> AllProducts()
    {
        return ProductList.ToList();
    }

    /// <summary>
    /// Opens stream reader to read bid data.
    /// </summary>
    public void LoadAllBids()
    {
        if (File.Exists(BidData))
        {
            using (StreamReader r = new StreamReader(BidData))
            {
                while (true)
                {
                    string Ident = r.ReadLine();

                    if (Ident == null || Ident == "")
                        break;
                    else
                    {
                        //int IdentInt = int.Parse(Ident);
                        LoadBid(Ident, r);
                    }

                }
            }
        }
    }

    /// <summary>
    /// Loads bid data from file.
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="r"></param>
    private void LoadBid(string hash, TextReader r)
    {

        Product FoundProduct = FindProductByHash(hash);

        string bidName = r.ReadLine();
        string bidEmail = r.ReadLine();
        string bidAmt_ = r.ReadLine();
        string delivopt = r.ReadLine();
        string unitnum_ = r.ReadLine();
        string streetnumber = r.ReadLine();
        string streetsuffix = r.ReadLine();
        string city = r.ReadLine();
        string postcode = r.ReadLine();
        string state = r.ReadLine();
        string starttime = r.ReadLine();
        string endtime = r.ReadLine();


        //addional valdation when file reading
        if (
            Validation.GetInputBool(bidEmail) &&
            Validation.GetInputBool(streetnumber) &&
            Validation.GetInputBool(streetsuffix) &&
            Validation.GetInputBool(city) &&
            Validation.GetInputBool(postcode) &&
            Validation.GetInputBool(state)
        //need to add double validation here 

        )
        {
            double bidAmt = double.Parse(bidAmt_);

            int unitnumInt = int.Parse(unitnum_);

            int streetnumInt = int.Parse(unitnum_);

            DateTime startDT = Convert.ToDateTime(starttime);

            DateTime endDT = Convert.ToDateTime(starttime);


            ClientAddress Address = new ClientAddress(bidEmail, unitnumInt, streetnumInt, streetsuffix, city, postcode, state);

            FoundProduct.BidList.Add(new Bid(FoundProduct, bidName, bidEmail, bidAmt, delivopt, Address, startDT, endDT));
        }
        else
        {
            Console.Error.WriteLine("File Read Failed");
        }
    }

    /// <summary>
    /// Saves bid to file.
    /// </summary>
    public void SaveClientsBids()
    {
        using (StreamWriter w = new StreamWriter(BidData))
        {
            foreach (Product product in ProductList)
            {

                foreach (Bid bid in product.BidList)
                {
                    w.WriteLine(bid.ProductCode);
                    w.WriteLine(bid.BidderName);
                    w.WriteLine(bid.BidderEmail);
                    w.WriteLine(bid.BidAmt);
                    w.WriteLine(bid.DeliveryOption);
                    w.WriteLine(bid.DeliveryAddress.UnitNumber);
                    w.WriteLine(bid.DeliveryAddress.StreetNumber);
                    w.WriteLine(bid.DeliveryAddress.StreetSuffix);
                    w.WriteLine(bid.DeliveryAddress.City);
                    w.WriteLine(bid.DeliveryAddress.Postcode);
                    w.WriteLine(bid.DeliveryAddress.State);
                    w.WriteLine(bid.StartDeliveryWindow);
                    w.WriteLine(bid.EndDeliveryWindow);

                }
            }

            w.Close();
        }
    }

    /// <summary>
    /// Gets products whose name matches the search phrase.
    /// </summary>
    /// <param name="Phrase"></param>
    /// <returns>List of matching Products.</returns>

    public List<Product> GetMatches(string Phrase, List<Product> productlist)
    {


        return productlist.Where(product => (product.ProductName).Contains(Phrase)).ToList();
    }

    public List<Product> GetMatchesD(string Phrase, List<Product> productlist)
    {
        return productlist.Where(product => (product.ProductDesc).Contains(Phrase)).ToList();
    }

    /// <summary>
    /// Get list of current clients products thats have bids on them.
    /// </summary>
    /// <param name="CurrentClient"></param>
    /// <returns>List of products with bids on them</returns>
    public List<Product> GetBids(Client CurrentClient)
    {
        List<Product> ProductList = CurrentLoggedInProducts(CurrentClient);
        //where bids exist

        List<Product> BidsList = new List<Product>();

        foreach (Product product in ProductList)
        {
            if (product.BidExists(product) == true)
            {
                BidsList.Add(product);
            }
        }
        return BidsList;
    }

    /// <summary>
    /// Gets product based on index of supplied list.
    /// </summary>
    /// <param name="productlist"></param>
    /// <param name="productindex"></param>
    /// <returns>Product with matching index.</returns>
    public Product GetProduct(List<Product> productlist, int productindex)
    {

        Product returnProduct = new Product(null, null, null, null);

        int index = 0;

        foreach (Product product in productlist)
        {
            index++;
            if (index == productindex)
            {
                returnProduct = product;
            }
        }

        return returnProduct;

    }

    /// <summary>
    /// Opens stream reader to load purchases.
    /// </summary>
    public void LoadAllPurchases()
    {
        if (File.Exists(PurchaseData))
        {
            using (StreamReader r = new StreamReader(BidData))
            {
                while (true)
                {
                    string Ident = r.ReadLine();

                    if (Ident == null || Ident == "")
                        break;
                    else
                    {
                        //LoadPurchase(Ident, r);
                    }

                }
            }
        }
    }

    /// <summary>
    /// Loads purchases and adds to active list.
    /// </summary>
    /// <param name="Ident"></param>
    /// <param name="r"></param>
    private void LoadPurchase(string Ident, TextReader r)
    {
        //returns clint with same email 
        Client client = Find(Ident);

        //returns address of clients purchase
        //ClientAddress address = FindAddress(Ident);

        string producthash = r.ReadLine();

        //int producthashInt = int.TryParse(producthash);

        Product product = FindProductByHash(producthash);

        // find by by hash and check if the highest 

        //addional valdation when file reading
        if (
            Validation.GetInputBool(Ident)
        //need to add double validation here 

        )
        {

        client.PurchasedProducts.Add(new Purchase(client, product, product.HighestBid(product)));
        }
        else
        {
            Console.Error.WriteLine("File Read Failed");
        }
    }

    /// <summary>
    /// Writes Purchases to a file 
    /// </summary>
    public void SaveClientsPurchases()
    {
        using (StreamWriter w = new StreamWriter(PurchaseData))
        {
            foreach (Client client in ClientList)
            {

                foreach (Purchase purchase in client.PurchasedProducts)
                {
                    w.WriteLine(purchase.Buyer.Email);
                    w.WriteLine(purchase.Purchaseditem.GetHashCode().ToString());
                }
            }

            w.Close();
        }
    }

    /// <summary>
    /// Saves a clients purchase and adds to active list
    /// </summary>
    /// <param name="client"></param>
    /// <param name="purchase"></param>
    public void SavePurchase(Client client, Purchase purchase)
    {
        client.PurchasedProducts.Add(purchase);
        SaveClientsPurchases();
    }









}
