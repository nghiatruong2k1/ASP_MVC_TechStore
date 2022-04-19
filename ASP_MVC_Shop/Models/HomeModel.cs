using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_MVC_Shop.Contexts;
namespace ASP_MVC_Shop.Models
{
    public class HomeModel
    {
        public List<ViewBrand_0256> listBrand { get; set; }
        public List<ViewCategory_0256> listCategory { get; set; }
        public List<ViewCategory_0256> listCategoryPopular { get; set; }
        public IPagedList<ViewProduct_0256> listProductDeal { get; set; }
        public IPagedList<ViewProduct_0256> listProductRecommend { get; set; }
    }
}