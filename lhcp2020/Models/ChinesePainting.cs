using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lhcp2020.Models
{
    public class ChinesePainting
    {
        [Key] 
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string Alt { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSize { get; set; }
        public string Categories { get; set; }
        public string ProductType { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string SizeDetail { get; set; }
        public DateTime AddedDate { get; set; }
        public string Status { get; set; }

        public string getLargeImageName(string imageURL)
        {
            return imageURL.Replace(".jpg", "_B.jpg");
        }

        public string LargeImageUrl
        {
            get
            {
                return getLargeImageName(ImageUrl);
            }
        }

    }
}
