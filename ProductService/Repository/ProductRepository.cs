using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Repository
{
    public class ProductRepository
    {
        #region Constructor
        private ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        #region GetAllProducts
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _dbContext.Products
                                 .AsNoTracking()
                                 .ToList();
            }
            catch (Exception)
            {
                // Consider logging the exception. Returning an empty list to keep the contract stable.
                return [];
            }
        }
        #endregion

        #region GetProductsByCategoryId
        public IEnumerable<Product> GetProductsByCategoryId(Guid id)
        {
            try
            {
                return _dbContext.Products
                                 .AsNoTracking()
                                 .Where(p => p.CategoryId == id)
                                 .ToList();
            }
            catch (Exception)
            {
                // Consider logging the exception. Returning an empty list to keep the contract stable.
                return new List<Product>();
            }
        }
        #endregion

        #region GetProductsById
        public Product? GetProductsById(Guid id)
        {
            try
            {
                return _dbContext.Products
                                 .AsNoTracking()
                                 .FirstOrDefault(p => p.Id == id);
            }
            catch (Exception)
            {
                // Consider logging the exception. Returning null to indicate failure/not found.
                return null;
            }
        }
        #endregion

        #region AddProduct
        public bool AddProduct(Product product)
        {
            try
            {
                if (product == null) return false;

                if (product.Id == Guid.Empty)
                    product.Id = Guid.NewGuid();

                // set created time if not set
                if (product.CreatedAt == default)
                    product.CreatedAt = DateTimeOffset.UtcNow;

                //check if product with same id already exists
                var existing = _dbContext.Products.Find(product.Id);
                if (existing != null) return false;

                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                // Consider logging the exception. Returning null to indicate failure.
                return false;
            }
        }

        #endregion

        #region AddProductsBulk
        public bool AddProductsBulk(IEnumerable<Product> products)
        {
            try
            {
                if(products == null || !products.Any()) return false;

                _dbContext.Products.AddRange(products);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region updateProduct
        public Product? UpdateProduct(Product product)
        {
            try
            {
                if (product == null || product.Id == Guid.Empty) return null;

                var existing = _dbContext.Products.Find(product.Id);
                if (existing == null) return null;

                // update fields -- copy allowed updatable properties
                existing.ProductName = product.ProductName;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.CategoryId = product.CategoryId;
                existing.IsAvailable = product.IsAvailable;
                // do not overwrite CreatedAt unless intentionally provided
                // existing.CreatedAt = product.CreatedAt;

                _dbContext.Entry(existing).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return existing;
            }
            catch (Exception)
            {
                // Consider logging the exception. Returning null to indicate failure.
                return null;
            }
        }
        #endregion

        #region DeleteProduct
        public bool DeleteProduct(Guid id)
        {
            try
            {
                var existing = _dbContext.Products.Find(id);
                if (existing == null) return false;

                _dbContext.Products.Remove(existing);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                // Consider logging the exception. Returning false to indicate failure.
                return false;
            }
        }
        #endregion


        #endregion
    }
}
