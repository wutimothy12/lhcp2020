using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lhcp2020.Models
{
    public class ChinesePaintingQueries
    {
        private lhcp2020Context source;
       
        public ChinesePaintingQueries(lhcp2020Context ctx)
        {
            source = ctx;
        }
        public  ChinesePainting GetProduct(int id)
        {
            return source.ChinesePaintings.SingleOrDefault(p => p.ProductID == id);
        }

        public  MainText GetMainText(string name)
        {
            return source.MainText.SingleOrDefault(t => t.ProductName == name);
        }

        public IQueryable<ChinesePainting> GetProducts()
        {
            return from p in source.ChinesePaintings
                           orderby p.ProductID
                           select p;
            
        }

        
        public int GetProductsCount()
        {
            return GetProducts().Count();
               

        }

        public  int GetProductsByCatergoryCount(string category)
        {
            return ByCatergory(category).Count();


        }

        public  IQueryable<ChinesePainting> ByCatergory(string category)
        {
            return from p in GetProducts()
                   where p.Categories == category
                   select p;
        }

       
        public  int GetProductsByNameCount(string name)
        {
            return ByName(name).Count();


        }

        public  IQueryable<ChinesePainting> ByName(string name)
        {
            return from p in GetProducts()
                   where p.ProductName == name
                   select p;
        }

        
        public  int GetProductsByTypeCount(string name)
        {
            return ByType(name).Count();


        }

        public  IQueryable<ChinesePainting> ByType(string name)
        {
            return from p in GetProducts()
                   where p.ProductType == name
                   select p;
        }

       
        public  int GetProductsByDateCount()
        {
            return ByDate().Count();


        }

        public  IQueryable<ChinesePainting> ByDate()
        {
            return from p in GetProducts()
                   //where p.AddedDate == Convert.ToDateTime("6/8/2010")
                   where p.AddedDate == Convert.ToDateTime("12/10/2007")
                   select p;
        }

        
        public  int GetProductsByPriceACount()
        {
            return ByPriceA().Count();


        }

        public  IQueryable<ChinesePainting> ByPriceA()
        {
            return from p in GetProducts()
                   where p.UnitPrice < 50
                   select p;
        }

        
        public  int GetProductsByPriceBCount()
        {
            return ByPriceB().Count();


        }

        public  IQueryable<ChinesePainting> ByPriceB()
        {
            return from p in GetProducts()
                   where p.UnitPrice >= 50 && p.UnitPrice <= 200
                   select p;
        }

        
        public  int GetProductsByPriceCCount()
        {
            return ByPriceC().Count();


        }

        public  IQueryable<ChinesePainting> ByPriceC()
        {
            return from p in GetProducts()
                   where p.UnitPrice >= 200 && p.UnitPrice <= 500
                   select p;
        }

       
        public  int GetProductsByPriceDCount()
        {
            return ByPriceD().Count();


        }

        public  IQueryable<ChinesePainting> ByPriceD()
        {
            return from p in GetProducts()
                   where p.UnitPrice > 500 
                   select p;
        }

        
       
    }
}
