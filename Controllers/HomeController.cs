using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using TechnicalStock.Models.ViewModels;
using TechnicalStock.Models;

namespace TechnicalStock.Controllers
{
    public class HomeController : Controller
    {
        private IStockRepository repository;
        public Cart ShoppingCart { get; set; }

        public HomeController(IStockRepository repo, Cart cartService)
        {
            repository = repo;
            ShoppingCart = cartService;
        }
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            ProductsFromCurrentCartAndStock viewResult = new ProductsFromCurrentCartAndStock();
            viewResult.CartSpares = ShoppingCart.Lines.Select(line => line.SparePart).ToList();
            viewResult.StockSpares = repository.SpareParts;
            if (!String.IsNullOrEmpty(searchString))
            {
                viewResult.StockSpares = viewResult.StockSpares.Where(s => s.Name.Contains(searchString));// || s.Description.Contains(searchString));
            }
            return View(viewResult);
        }

        //Для страницы с детальной информацией о выбранной зап. части
        public IActionResult Details(int id)
        {
            ProductsFromCurrentCartAndStock viewResult = new ProductsFromCurrentCartAndStock();
            viewResult.CartSpares = ShoppingCart.Lines.Select(line => line.SparePart).ToList();
            viewResult.StockSpares = repository.SpareParts.Where(spare => spare.SparePartId == id);
            return View(viewResult);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description, int quantity, string[] simulator)
        {
            SparePart newPart = new SparePart { 
                Name = name, 
                Description = description, 
                Quantity = quantity, 
                Simulator = string.Join(", ", simulator)};
            repository.AddNewItem(newPart);
            return View();//TODO: выводить сообщение об успехе добавления
        }

        
    }
}
