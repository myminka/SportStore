using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Repo;
using System.Linq;

namespace SportsStore.Components
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;

        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        [HttpGet]
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return View("Completed", order.ID);
            }

            return View();
        }

    }
}
