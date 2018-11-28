namespace Shared
{
    public class Person
    {
        public string SocialSecurityNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Complaint { get; set; }

        public Person(string socialSecurityNumber, string name, string address, string complaint)
        {
            this.SocialSecurityNumber = socialSecurityNumber;
            this.Name = name;
            this.Address = address;
            this.Complaint = complaint;
        }
    }
}