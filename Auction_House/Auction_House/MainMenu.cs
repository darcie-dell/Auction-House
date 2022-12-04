namespace Action_house
{
    class MainMenu : Menu
    {
        /// <summary>
        /// Class that adds functional menus, One for when a client is currently logged out (main) and one for when a client is logged in (client menu).
        /// </summary>

        private string Welcome_Title = "+-----------------------------+\n| Welcome to the Auction House |\n+-----------------------------+\n";


        private string Main_Title = "\nMain Menu\n---------";

        private string Main_Prompt = "\nPlease select an option between 1 and 3";

        private string Client_Title = "\nClient Menu\n-----------";

        private string Client_Prompt = "\nPlease select an option between 1 and 6";

        /// <summary>
        /// Starts the instance of the Auction house by creating a Database, aswell as Menus that display according to the current logged in client token.
        /// </summary>
        public void Start()
        {
            Console.WriteLine(Welcome_Title);
            Database Database = new Database();
            Menu MainMenu = new Menu();
            Menu ClientMenu = new Menu();

            MainMenu.Add("Register", Database.Register);
            MainMenu.Add("Sign in", Database.Login);
            MainMenu.Add("Exit", Database.Exit);

            ClientMenu.Add("Advertise Product", Database.AdvProduct);
            ClientMenu.Add("View My Product List", Database.ViewMyProducts);
            ClientMenu.Add("Search For Advertised Products", Database.SearchProducts);
            ClientMenu.Add("View Bids On My Products", Database.ViewBids);
            ClientMenu.Add("View My Purchased Items", Database.ViewMyPurchases);
            ClientMenu.Add("Log off", Database.Logout);

            while (Database.CurrentClient == null)
            {
                MainMenu.Display(Main_Title, Main_Prompt);
                while (Database.CurrentClient != null)
                {
                    ClientMenu.Display(Client_Title, Client_Prompt);
                }
            }
        }

    }
}

