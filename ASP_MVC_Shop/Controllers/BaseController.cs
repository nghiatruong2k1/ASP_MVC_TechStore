using ASP_MVC_Shop.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Shop.Controllers
{
    public class BaseController : Controller
    {
        protected Database_TTTN_0256Entities objEntity = new Database_TTTN_0256Entities();
        protected Common common;
        protected List<Toast> lstToast;
        public BaseController() : base()
        {
            common = new Common();
            lstToast = TempData["toast"] as List<Toast>;
            if (lstToast == null)
            {
                lstToast = new List<Toast>();
            }
        }
        protected void EndAction()
        {
            TempData["toast"] = this.lstToast;
        }
    }
}