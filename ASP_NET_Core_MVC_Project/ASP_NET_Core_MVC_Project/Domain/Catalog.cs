using ASP_NET_Core_MVC_Project.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace ASP_NET_Core_MVC_Project.Domain
{
    /// <summary>
    /// Потокобезопасный каталог
    /// </summary>
    public class Catalog
    {

        private ConcurrentDictionary<int, Product> Products { get; set; }
        public Catalog()
        {
            Products = new ConcurrentDictionary<int, Product>();
        }

        public void AddProduct(Product product, CancellationToken cancellationToken)
        {
            Products.TryAdd(product.Id, product);
        }

        public void DeleteProduct(Product product, CancellationToken cancellationToken)
        {
            Products.TryRemove(product.Id, out _);
        }

        public int CountProducts(CancellationToken cancellationToken)
        {
            return Products.Count;
        }

        public Product? FindProduct(int id, CancellationToken cancellationToken)
        {
            return Products[id];
        }

        public void UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            Products.TryUpdate(product.Id, product, Products[product.Id]);
        }
        public List<Product> GetProducts()
        {
            return Products.Values.ToList();
        }
    }
}
