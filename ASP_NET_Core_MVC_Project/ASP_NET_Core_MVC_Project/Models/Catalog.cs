namespace ASP_NET_Core_MVC_Project.Models
{
    public class Catalog
    {
        public List<Product> Products { get; set; }
        public Catalog()
        {
            Products = new List<Product>();
        }
    }
}
