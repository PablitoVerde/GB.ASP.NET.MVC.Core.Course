using ASP_NET_Core_MVC_Project.Interfaces;


namespace ASP_NET_Core_MVC_Project.Domain.Events
{
    public class ProductAdded : IDomainEvent
    {
        public Product Product { get; }

        public DateTime TimeOfSet { get; }

        public ProductAdded(Product product)
        {
            Product = product;
            TimeOfSet = DateTime.Now;
        }
    }
}
