using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrasturcture;
using SportsStore.Models;
using SportsStore.Models.Repo;
using SportsStore.Models.ViewModels;
using System;
using System.Linq;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IStoreRepository repository;
        private readonly Cart cart;

        public CartController(IStoreRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = cart ?? new Cart(),
                ReturnUrl = returnUrl ?? "/"
            });
        }

        [HttpPost]
        public IActionResult Index(long productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            cart.AddItem(product, 1);
            return View(new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public IActionResult Remove(long productId, string returnUrl)
        {
            cart.RemoveLine(cart.Lines.First(cl => cl.Product.ProductId == productId).Product);

            return View("Index", new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl ?? "/"
            });
        }
    }
}
