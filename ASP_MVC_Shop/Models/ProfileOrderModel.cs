using ASP_MVC_Shop.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_Shop.Models
{
    public class ProfileOrderModel
    {
        public ViewOrder_0256 order { get; set; }
        public List<ViewOrderDetail_0256> products { get; set; }
    }
}