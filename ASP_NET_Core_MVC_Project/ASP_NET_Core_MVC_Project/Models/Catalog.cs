using System.Collections.Concurrent;

namespace ASP_NET_Core_MVC_Project.Models
{
    /// <summary>
    /// Потокобезопасный каталог
    /// </summary>
    public class Catalog
    {

        private ConcurrentDictionary<int, Product> Products { get; set; }
        private object _lock = new object();

        public Catalog()
        {
            Products = new ConcurrentDictionary<int, Product>();
        }

        public void AddProduct(Product product)
        {
            Products.TryAdd(product.Id, product);
        }

        public void DeleteProduct(Product product)
        {
            Products.TryRemove(product.Id, out _);
        }

        public int CountProducts()
        {
            return Products.Count();
        }

        public Product? FindProduct(int id)
        {
            return Products[id];
        }

        public void UpdateProduct(Product product)
        {
            Products.TryUpdate(product.Id, product, Products[product.Id]);
        }
        public List<Product> GetProducts()
        {
                return Products.Values.ToList();
        }
    }
}
