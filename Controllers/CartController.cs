using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechnicalStock.Models;

namespace TechnicalStock.Controllers
{
    public class CartController : Controller
    {
        private IStockRepository repository;
        public CartController(IStockRepository repo, Cart cartService)
        {
            repository = repo;
            ShoppingCart = cartService; //Получаем состояние корзины

        }
        //Корзина
        public string ReturnUrl { get; set; }
        public Cart ShoppingCart { get; set; }



        [HttpGet]
        public IActionResult CartIndex(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            ViewData["returnUrl"] = ReturnUrl;
            return View(ShoppingCart);
        }

        [HttpPost]
        public IActionResult CartIndex(int sparePartId, string returnUrl, int qty = 1)
        {
            SparePart sparePart = repository.SpareParts.FirstOrDefault(p => p.SparePartId == sparePartId);
            ShoppingCart.AddItem(sparePart, qty);
            return RedirectToActionPermanent("Index", "Home");
            //return RedirectToPage(returnUrl);
        }

        [HttpPost]
        public IActionResult RemoveItemFromCart(int sparePartId, string returnUrl)
        {
            ShoppingCart.RemoveLine(ShoppingCart.Lines.First(cl => cl.SparePart.SparePartId == sparePartId).SparePart);
            return RedirectToActionPermanent("CartIndex", new { returnUrl }); //После удаления перенаправляем на метод CartIndex [HttpGet]
        }
    }
}
