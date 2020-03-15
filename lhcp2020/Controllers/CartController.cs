using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lhcp2020.Models;

namespace lhcp2020.Controllers
{
    public class CartController : Controller
    {
        private ChinesePaintingQueries repository;
        private Cart cart;

        public CartController(ChinesePaintingQueries repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            ChinesePainting product = repository.GetProducts()
                .FirstOrDefault(p => p.ProductID == productId);

            if (product.Status == "Sold")
                return RedirectToAction("errormessage");

            foreach (var art in cart.Lines)
            {
                if (art.Product.ProductID == product.ProductID)
                { return RedirectToAction("errormessage"); }
            }

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId,
                string returnUrl)
        {
            ChinesePainting product = repository.GetProducts()
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult AddShipping()
        {
            cart.AddShipping();
            TempData["Anchor"] = "jump";
            return RedirectToAction("Index");
        }
        public RedirectToActionResult NoShipping()
        {
            cart.NoShipping();
            TempData["Anchor"] = "jump";
            return RedirectToAction("Index");
        }
        public RedirectToActionResult AddTax()
        {
            cart.AddTax();
            TempData["Anchor"] = "jump";
            return RedirectToAction("Index");
        }
        public RedirectToActionResult NoTax()
        {
            cart.NoTax();
            TempData["Anchor"] = "jump";
            return RedirectToAction("Index");
        }
        public RedirectToActionResult Promotion(string Code)
        {
            if (Code != "438645669")
                ModelState.AddModelError("code", "No promotion has this coupon code. Please try again.");
            if (!ModelState.IsValid)
            {
                TempData["message"] = "No promotion has this coupon code. Please try again.";
                TempData["Anchor"] = "jump";
                return RedirectToAction("Index");
            }

            cart.PromotionCode();
            TempData["Anchor"] = "jump";
            return RedirectToAction("Index");
        }

        public ViewResult ThankYou()
        {
            cart.Clear();
            return View();
        }

        public ViewResult errormessage()
        {
            return View();
        }

    }
}