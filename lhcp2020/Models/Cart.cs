using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lhcp2020.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(ChinesePainting product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(ChinesePainting product) =>
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Product.UnitPrice * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();
        public virtual void AddShipping() => Shipping = true;
        public virtual void NoShipping() => Shipping = false;
        public virtual void AddTax() => Tax = true;
        public virtual void NoTax() => Tax = false;
        public virtual void PromotionCode() => Promotion = true;

        public virtual IEnumerable<CartLine> Lines => lineCollection;

        public Boolean Tax { get; set; }
        public Boolean Shipping { get; set; }
        public Boolean Promotion { get; set; }
        public decimal Discount { get; set; }
        
        /// <summary>
        /// Gets the sub total.
        /// </summary>
        /// <value>The sub total.</value>
        public decimal SubTotal
        {
            get
            {
                decimal totalPrice = 0M;
                decimal discount = 0M;
                foreach (var item in lineCollection)
                { totalPrice += item.TotalPrice; }

                //if (Promotion == true)
                //    totalPrice = decimal.Round(decimal.Multiply(totalPrice, .80m), 2);

                if (Promotion == true)
                {
                    discount = decimal.Round(decimal.Multiply(totalPrice, .80m), 2);
                    Discount = totalPrice - discount;
                    totalPrice = discount;
                }


                return totalPrice;
            }
        }

        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        /// <value>The total price.</value>
        public decimal Total
        {
            get
            {
                decimal totalPrice = SubTotal;


                if (Tax == true)
                    totalPrice = decimal.Round(decimal.Multiply(totalPrice, 1.095m), 2);
                if (Shipping == true)
                    totalPrice += 55;


                return totalPrice;
            }
        }

        public decimal ShippingAndTax
        {
            get
            {
                decimal totalPrice = Total - SubTotal;

                return totalPrice;
            }
        }
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public ChinesePainting Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.UnitPrice * Quantity;
    }
}
