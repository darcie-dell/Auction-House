namespace Action_house
{
    public class ClientAddress
    {
        /// <summary>
        /// Client Address class that creates an address object associated with a client by email address.
        /// </summary>

        public string Email { get; private set; }

        public int? UnitNumber { get; private set; }

        public int? StreetNumber { get; private set; }

        public string StreetSuffix { get; private set; }

        public string City { get; private set; }

        public string Postcode { get; private set; }

        public string State { get; private set; }

        public ClientAddress(
            string clientemail,
            int? unitnumber,
            int? streetnumber,
            string streetsuffix,
            string city,
            string postcode,
            string state
        )
        {
            this.Email = clientemail;
            this.UnitNumber = unitnumber;
            this.StreetNumber = streetnumber;
            this.StreetSuffix = streetsuffix;
            this.City = city;
            this.Postcode = postcode;
            this.State = state;

        }

        /// <summary>
        /// Returns bool based on the existence of an email in an Address. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public bool SameAs(object obj)
        {
            return Email == ((ClientAddress)obj).Email;
        }
    }
}
