using ASP_MVC_Shop.Models;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ASP_MVC_Shop.Contexts;
using System;

namespace ASP_MVC_Shop.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        [Route("chi-tiet-san-pham/{alias?}")]
        public ActionResult Detail(string alias)
        {

            ProductDetailModel productDetailModel = new ProductDetailModel();
            productDetailModel.product = objEntity.ViewProduct_0256.Where(p => p.alias.ToLower().Equals(alias.ToLower())).FirstOrDefault();
            return View(productDetailModel);
        }
        [Route("san-pham-danh-muc/{alias}/{page:int?}")]
        public ActionResult Category(string alias = "", int page = 1,int limit = 8,int view = 0, Boolean isPartical = false)
        {

            ProductListModel productListModel = new ProductListModel();
            List<ViewProduct_0256> viewProducts = new List<ViewProduct_0256>();
            Category_0256 props = new Category_0256();

            if(!alias.ToLower().Equals("xem-tat-ca"))
            {
                props = objEntity.Category_0256.Where(p => p.alias.ToLower().Equals(alias.ToLower())).FirstOrDefault();
                productListModel.name = props.name;
                viewProducts = objEntity.ViewProduct_0256.Where(p => p.categoryId == props.id).ToList();
            }
            else
            {
                productListModel.name = "Tất cả sản phẩm";
                viewProducts = objEntity.ViewProduct_0256.ToList();
            }
            
            productListModel.view = view;
            productListModel.alias = alias;
            productListModel.action = "category";
            productListModel.controller = "product";



            productListModel.listCategory = objEntity.Category_0256.ToList();
            productListModel.listBrand = objEntity.Brand_0256.ToList();

            productListModel.totalItem = viewProducts.Count;

            productListModel.listProduct = viewProducts.ToPagedList(page, limit);

            if (isPartical)
            {
                return PartialView(productListModel);
            }
            else
            {
                return View(productListModel);
            }
        }
        [Route("san-pham-thuong-hieu/{alias?}/{page:int?}")]
        public ActionResult Brand(string alias = "", int page = 1, int limit = 8,int view = 0, Boolean isPartical = false)
        {

            ProductListModel productListModel = new ProductListModel();
            Brand_0256 props = new Brand_0256();

            if (!alias.ToLower().Equals("xem-tat-ca"))
            {
                props = objEntity.Brand_0256.Where(p => p.alias.ToLower().Equals(alias.ToLower())).FirstOrDefault();
                productListModel.name = props.name;
            }
            else
            {
                productListModel.name = "Tất cả sản phẩm";
            }
            productListModel.view = view;
            productListModel.alias = alias;
            productListModel.action = "brand";
            productListModel.controller = "product";

            productListModel.listCategory = objEntity.Category_0256.ToList();
            productListModel.listBrand = objEntity.Brand_0256.ToList();

            List<ViewProduct_0256> viewProducts = new List<ViewProduct_0256>();
            if (props.id > 0)
            {
                viewProducts = objEntity.ViewProduct_0256.Where(p => p.brandId == props.id).ToList();
            }
            else
            {
                viewProducts = objEntity.ViewProduct_0256.ToList();
            }
            productListModel.totalItem = viewProducts.Count;


            productListModel.listProduct = viewProducts.ToPagedList(page, limit);


            if (isPartical)
            {
                return PartialView(productListModel);
            }
            else
            {
                return View(productListModel);
            }
        }

        [Route("tim-kiem-san-pham/{query?}/{page:int?}")]
        public ActionResult Search(string query = "", int page = 1, int limit = 8, int view = 0, Boolean isPartical = false)
        {
            ProductListModel productListModel = new ProductListModel();
            if (query.Trim().Equals(""))
            {
                return Redirect("/");
            }
            else
            {
                productListModel.name = query;
                productListModel.query = query;
                productListModel.view = view;
                productListModel.action = "search";
                productListModel.controller = "product";

                productListModel.listCategory = objEntity.Category_0256.ToList();
                productListModel.listBrand = objEntity.Brand_0256.ToList();

                string queryLower = query.ToLower();
                string queryAlias = common.getAlias(query);


                List<ViewProduct_0256> viewProducts = objEntity.ViewProduct_0256.Where(p => 
                    (
                        p.name.ToLower().Contains(queryLower)
                        || p.shortDes.ToLower().Contains(queryLower)
                        || p.alias.Contains(queryAlias)
                        || p.brandName.ToLower().Contains(queryLower)
                        || p.categoryName.ToLower().Contains(queryLower)
                    )
                    && p.isTrash != true
                ).ToList();

                productListModel.totalItem = viewProducts.Count;
                productListModel.listProduct = viewProducts.ToPagedList(page, limit);

                if (isPartical)
                {
                    return PartialView(productListModel);
                }
                else
                {
                    return View(productListModel);
                }
            }
        }
    }
}