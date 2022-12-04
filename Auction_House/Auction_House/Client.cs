namespace Action_house
{
    /// <summary>
    /// Client class that creates clients, contains a list of purchased products not yet implemented.
    /// </summary>
    public class Client
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Pwd { get; private set; }

        public List<Purchase> PurchasedProducts { get; protected set; } = new List<Purchase>();

        public Client(string name, string email, string pwd)
        {
            this.Name = name;
            this.Email = email;
            this.Pwd = pwd;

        }

        /// <summary>
        /// Returns bool based on the existence of an email in a client object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public bool SameAs(object obj)
        {
            return Email == ((Client)obj).Email;
        }

        /// <summary>
        /// Displays name and email in desired format. 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"{Name}({Email})";
        }

        /// <summary>
        /// Adds a clients purchase to list.
        /// </summary>
        /// <param name="product"></param>
        public void AddToPuchased(Purchase product)
        {
            PurchasedProducts.Add(product);
        }
    }
}
