using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
/// <summary>
/// Класс берет на себя создание объекта корзины, его редактирование за счет
/// базового класса, запись и удаление данных в json;
/// </summary>
namespace TechnicalStock.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(SparePart sparePart, int quantity)
        {
            base.AddItem(sparePart, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(SparePart sparePart)
        {
            base.RemoveLine(sparePart);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
