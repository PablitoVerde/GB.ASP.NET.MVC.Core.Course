namespace ASP_NET_Core_MVC_Project.Models
{
    /// <summary>
    /// Потокобезопасный каталог
    /// </summary>
    public class Catalog
    {
        private List<Product> Products { get; set; }
        private object objectToSynchronise = new object();
       
        /// <summary>
        /// Стандартный конструктор по созданию объекта класса Каталог
        /// </summary>
        public Catalog()
        {
            Products = new List<Product>();
        }

        /// <summary>
        /// Добавить товар в список
        /// </summary>
        /// <param Товар="product"></param>
        public void AddProduct(Product product)
        {
            lock (objectToSynchronise)
            {
                Products.Add(product);
            }
        }

        /// <summary>
        /// Удалить товар из списка. В случае, если товара нет, удаление проведено не будет.
        /// </summary>
        /// <param Товар="product"></param>
        public void DeleteProduct(Product product)
        {
            lock (objectToSynchronise)
            {
                Products = Products.FindAll(x => x.Id != product.Id).ToList();
            }
        }

        /// <summary>
        /// Подсчитать количество товаров в каталоге
        /// </summary>
        /// <returns></returns>
        public int CountProducts()
        {
            lock (objectToSynchronise)
            {
                return Products.Count;
            }
        }

        /// <summary>
        /// Найти товар в каталоге по идентификатору товара. В случае отсутствия товара будет возвращен первый в каталоге. Если каталог пустой - вернется новый пустой товар с идентификатором 0.
        /// </summary>
        /// <param Идентификатор="id"></param>
        /// <returns></returns>
        public Product FindProduct(int id)
        {
            lock (objectToSynchronise)
            {
                Product result = Products.Find(x => x.Id == id);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    if (CountProducts() == 0)
                    {
                        return new Product();
                    }
                    else
                    {
                        return FindProduct(0);
                    }
                }
            }
        }

        /// <summary>
        /// Обновить товар
        /// </summary>
        /// <param Товар="product"></param>
        public void UpdateProduct(Product product)
        {
            lock (objectToSynchronise)
            {
                foreach (Product p in Products)
                {
                    if (p.Id == product.Id)
                    {
                        p.Name = product.Name;
                        p.Description = product.Description;
                        p.LinkToImage = product.LinkToImage;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Получить потокобезопасно список товаров
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            lock(objectToSynchronise)
            {
                return Products;
            }
        }
    }
}
