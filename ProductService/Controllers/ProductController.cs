using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        #region Constructor
        private Repository.ProductRepository _productRepository;
        public ProductController(Repository.ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods

        #region GetAllProducts
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var result = _productRepository.GetAllProducts();
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion

        #region GetProductsByCategoryId
        [HttpGet("category/{categoryId:guid}")]
        public IActionResult GetProductsByCategoryId(Guid categoryId)
        {
            try
            {
                if (categoryId == Guid.Empty)
                    return Json(new { success = false, message = "Invalid category id." });

                var result = _productRepository.GetProductsByCategoryId(categoryId);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion

        #region GetProductsById
        [HttpGet("{id:guid}")]
        public IActionResult GetProductsById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return Json(new { success = false, message = "Invalid product id." });

                var product = _productRepository.GetProductsById(id);
                if (product == null)
                    return Json(new { success = false, message = "Product not found." });

                return Json(product);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion

        #region AddProduct
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                if (product == null)
                    return Json(new { success = false, message = "Product payload is required." });

                var added = _productRepository.AddProduct(product);
                if (added == false)
                    return Json(new { success = false, message = "Failed to add product." });

                return Json(added);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion

        #region AddProductsBulk
        [HttpPost("Bulk")]
        public IActionResult AddProductsBulk(List<Product> products)
        {
            try
            {
                bool result = _productRepository.AddProductsBulk(products);
                if(result) return Json(new { success = true, message = "Products added successfully." });
                else return Json(new { success = false, message = "Failed to add products. DAL" });
            }
            catch (Exception)
            {
                return Json("Error from Service Layer!");
            }
        }
        #endregion

        #region UpdateProduct
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null || product.Id == Guid.Empty)
                    return Json(new { success = false, message = "Valid product with Id is required." });

                var updated = _productRepository.UpdateProduct(product);
                if (updated == null)
                    return Json(new { success = false, message = "Failed to update product or product not found." });

                return Json(updated);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteProduct(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return Json(new { success = false, message = "Invalid product id." });

                var deleted = _productRepository.DeleteProduct(id);
                return Json(new { success = deleted });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        #endregion

        #endregion
    }
}
