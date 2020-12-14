using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vic.SportsStore.Domain.Abstract;
using Vic.SportsStore.Domain.Entities;
using Vic.SportsStore.WebApp.Models;

namespace Vic.SportsStore.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _repository;

        public const int PageSize = 5;

        public ProductController(IProductsRepository productsRepository)
        {
            this._repository = productsRepository;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string category, int page = 1)
        {
            var categoryProducts = _repository
                .Products
                .Where(p => category == null || p.Category == category);

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = categoryProducts
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryProducts.Count()
                },

                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = _repository
                .Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


    }
}