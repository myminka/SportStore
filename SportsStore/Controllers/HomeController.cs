using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using SportsStore.Models.Repo;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#pragma warning disable S4487

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository _repository;

        public const int PageSize = 4;

        public HomeController(ILogger<HomeController> logger, IStoreRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public ViewResult Index(string category, int productPage = 1)
         => View(new ProductsListViewModel
         {
             Products = _repository.Products
                   .Where(p => category == null || p.Category == category)
                   .OrderBy(p => p.ProductId)
                   .Skip((productPage - 1) * PageSize)
                   .Take(PageSize),
             PagingInfo = new PagingInfo
             {
                 CurrentPage = productPage,
                 ItemsPerPage = PageSize,
                 TotalItems = category == null ?
                       _repository.Products.Count() :
                       _repository.Products.Where(e =>
                           e.Category == category).Count()
             },
             CurrentCategory = category
         });

    }
}
