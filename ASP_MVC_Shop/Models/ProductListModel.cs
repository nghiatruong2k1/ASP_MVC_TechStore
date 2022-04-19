using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_MVC_Shop.Contexts;
namespace ASP_MVC_Shop.Models
{
    public class ProductListModel
    {
        public int view {get;set;}

        public int totalItem { get; set; }
        public string action { get; set; }
        public string controller { get; set; }
        public string name { get; set; }
        public string query { get; set; }
        public string alias { get; set; }


        public List<Category_0256> listCategory { get; set; }
        public List<Brand_0256> listBrand { get; set; }
        public IPagedList<ViewProduct_0256> listProduct { get; set; }
    }
}