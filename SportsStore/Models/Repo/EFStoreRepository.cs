using System.Linq;

namespace SportsStore.Models.Repo
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => this.context.Products;

        public void CreateProduct(Product p)
        {
            context.Products.Add(p);
            context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            context.Products.Remove(p);
            context.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }
    }
}