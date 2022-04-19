using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_MVC_Shop.Contexts;
namespace ASP_MVC_Shop.Models
{
    public class CartModel
    {
        public CartModel(ViewProduct_0256 product,int quantity)
        {
            this.quantity = quantity;
            this.id = product.id;
            this.name = product.name;
            this.alias = product.alias;
            this.price = product.price;
            this.salePrice = product.salePrice;
            this.imageUrl = product.imageUrl;
            this.typeName = product.typeName;
            this.categoryName = product.categoryName;
            this.brandName = product.brandName;
        }
        public int quantity { get;set; }
        public int id { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> salePrice { get; set; }
        public string currencyUnit { get; set; }
        public string categoryName { get; set; }
        public string brandName { get; set; }
        public string imageUrl { get; set; }
        public Nullable<int> rating { get; set; }
        public string typeName { get; set; }
    }
}