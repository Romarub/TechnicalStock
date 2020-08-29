using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechnicalStock.Models;

namespace TechnicalStock.Controllers
{
    public class OrderController : Controller
    {
        private IStockRepository repository;
        private IOrderRepository orderRepository;

        public Cart ShoppingCart { get; }

        public OrderController(IStockRepository repo, IOrderRepository orderRepo, Cart cartService)
        {
            repository = repo;
            orderRepository = orderRepo;
            ShoppingCart = cartService; //Получаем состояние корзины
        }
        public IActionResult Checkout()
        {
            if (ShoppingCart.Lines.Count == 0)
            {
                return Content("Нет комплектующих в корзине");
            }
            else
            {
                //Добавляем запись в БД о новом заказе
                Order order = new Order();
                order.Lines = ShoppingCart.Lines;
                orderRepository.SaveOrder(order);

                //уменьшаем количество запасов в БД на складе
                SparePart sparePart = new SparePart();
                foreach (var line in ShoppingCart.Lines) //изменяем количество зап. частей в БД
                {
                    sparePart = repository.SpareParts.FirstOrDefault(sp => sp.SparePartId == line.SparePart.SparePartId);
                    sparePart.Quantity -= line.Quantity;
                    repository.UpdateItem(sparePart);
                }
                ShoppingCart.Clear();
            }
            return View();
        }
    }
}
