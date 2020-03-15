using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace lhcp2020.Models
{
    public class SessionCart : Cart
    {

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(ChinesePainting product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(ChinesePainting product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void AddShipping()
        {
            base.AddShipping();
            Session.SetJson("Cart", this);
        }
        public override void NoShipping()
        {
            base.NoShipping();
            Session.SetJson("Cart", this);
        }
        public override void AddTax()
        {
            base.AddTax();
            Session.SetJson("Cart", this);
        }
        public override void NoTax()
        {
            base.NoTax();
            Session.SetJson("Cart", this);
        }
        public override void PromotionCode()
        {
            base.PromotionCode();
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
