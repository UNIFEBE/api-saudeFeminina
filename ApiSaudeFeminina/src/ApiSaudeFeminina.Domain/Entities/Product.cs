namespace ApiSaudeFeminina.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        protected Product() { }

        public Product(string name, decimal price, int stockQuantity)
        {
            SetName(name);
            SetPrice(price);
            StockQuantity = stockQuantity;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome do produto nao pode ser vazio.");

            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("O preco nao pode ser negativo.");

            Price = price;
        }

        public void UpdateStock(int quantity)
        {
            StockQuantity = quantity;
            MarkAsUpdated();
        }
    }
}
