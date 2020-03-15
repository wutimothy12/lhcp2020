using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lhcp2020.Models;
using Microsoft.AspNetCore.Mvc;

namespace lhcp2020.Controllers
{
    public class ChinesepaintingController : Controller
    {
        private ChinesePaintingQueries lhcp;
   
        public int PageSize = 30;
        public ChinesepaintingController(ChinesePaintingQueries repocp)
        {
           lhcp = repocp;
        }
        public IActionResult Detail(int id)
        {
            var cp = lhcp.GetProduct(id);
            ViewBag.Title = "Chinese " + cp.ProductName.ToString() + " Painting";
            return View(cp);
        }

       
        public ActionResult Index(int page = 1)
        {
            //var viewData = lhcp.GetProducts(page);
            ViewBag.Title = "All Paintings - LHChinesepaintings";
            ViewData["MainText"] = "Chinese painting has become very popular in the western world, particularly the United States. Chinese painting is known for its beautiful landscapes and paintings of mammals, birds, and fish. In Chinese painting there are two basic techniques, ¡°meticulous¡± or Gong-bi and ¡°freehand¡± or Shui-mo. Meticulous is very detailed while freehand is more impressionistic. Most Chinese consider landscape paintings as the top example of Chinese art, and many westerners agree.";
            return View(new PaintingsListViewModel
            {
                CPaintings = lhcp.GetProducts()
                .Skip((page - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsCount(), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsCount()
                }
            });
        }
        public ActionResult bycategory(string category, int page)
        {
            
          
            var mtext = lhcp.GetMainText(category);
            ViewBag.Title = "Chinese " + category.ToString() + " Paintings";
            ViewData["MainText"] = mtext.MainTextDescription;
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByCatergory(category)
                .Skip((page - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByCatergoryCount(category), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByCatergoryCount(category)
                }
            });

        }
       
        public ActionResult byname(string name, int page)
        {
            
            var mtext = lhcp.GetMainText(name);
            ViewBag.Title = "Chinese " + name.ToString() + " Paintings";
            ViewData["MainText"] = mtext.MainTextDescription;
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByName(name)
               .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByNameCount(name), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByNameCount(name)
                }
            });

        }
        public ActionResult bytype(string name, int page)
        {
            
           
            var mtext = lhcp.GetMainText(name);
            ViewBag.Title = "Chinese " + name.ToString() + " Paintings";
            ViewData["MainText"] = mtext.MainTextDescription;
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByType(name)
               .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByTypeCount(name), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByTypeCount(name)
                }
            });

        }
        public ActionResult bydate(int page)
        {


            ViewBag.Title = "Chinese New Arrival Paintings";
            ViewData["MainText"] = "New 529 Chinese Paintings";
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByDate()
               .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByDateCount(), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByDateCount()
                }
            });

        }
        public ActionResult bypriceA(int page)
        {


            ViewBag.Title = "All Paintings Under $50";
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByPriceA()
               .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByPriceACount(), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByPriceACount()
                }
            });
        }
        public ActionResult bypriceB(int page)
        {


            ViewBag.Title = "All Paintings Between $50 ~ $200";
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByPriceB()
               .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByPriceBCount(), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByPriceBCount()
                }
            });
        }
        public ActionResult bypriceC(int page)
        {


            ViewBag.Title = "All Paintings Between $200 ~ $500";
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByPriceC()
                .Skip((page - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByPriceCCount(), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByPriceCCount()
                }
            });
        }
        public ActionResult bypriceD(int page)
        {


            ViewBag.Title = "All Paintings Over $500";
            return View("Index", new PaintingsListViewModel
            {
                CPaintings = lhcp.ByPriceD()
               .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo(@lhcp.GetProductsByPriceDCount(), page, PageSize, 10)
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = lhcp.GetProductsByPriceDCount()
                }
            });
        }
       
    }
}