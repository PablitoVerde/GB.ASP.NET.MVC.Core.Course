namespace ASP_NET_Core_MVC_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LinkToImage { get; set; }

        public Product()
        {
            Id = 0;
            Name = "";
            Description = "";
            LinkToImage = "";
        }
    }
}
