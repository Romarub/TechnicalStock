using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using TechnicalStock.Models;

namespace TechnicalStock.Controllers
{
    public class HomeController : Controller
    {
        private IStockRepository repository;


        public HomeController(IStockRepository repo, Cart cartService)
        {
            repository = repo;
           
        }
        public IActionResult Index()
        {
            return View(repository.SpareParts);
        }
        //Для страницы с детальной информацией о выбранной зап. части
        public IActionResult Details(int id)
        {
            SparePart sparePart = repository.SpareParts.FirstOrDefault(spare => spare.SparePartId == id);
            return View(sparePart);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string description, int quantity, string simulator)
        {
            SparePart newPart = new SparePart { Name = name, Description = description, Quantity = quantity, Simulator = simulator };
            repository.AddNewItem(newPart);
            return View();//TODO: выводить сообщение об успехе добавления
        }

        
    }
}
