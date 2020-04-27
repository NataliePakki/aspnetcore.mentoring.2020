namespace ConsoleAPIClient.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public double UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public Category Category { get; set; }

        public Supplier Supplier { get; set; }

        public override string ToString()
        {
            return $"Name: {this.ProductName};" +
                $" QuantityPerUnit: {this.QuantityPerUnit};" +
                $" UnitPrice: {this.UnitPrice};" +
                $" UnitsInStock: {this.UnitsInStock};" +
                $" Category: {this.Category.CategoryName};" +
                $" Supplier: {this.Supplier.CompanyName};";

        }
    }
}
