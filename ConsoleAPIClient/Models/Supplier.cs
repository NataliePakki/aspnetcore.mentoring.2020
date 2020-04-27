namespace ConsoleAPIClient.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }

        public string CompanyName { get; set; }

        public override string ToString()
        {
            return $"Name: {this.CompanyName};";
        }
    }
}
