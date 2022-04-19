using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_Shop.Master
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public Nullable<int> imageId { get; set; }
        public Nullable<int> orderIndex { get; set; }
        public Nullable<bool> isPopular { get; set; }
        public Nullable<bool> isPublic { get; set; }
        public Nullable<bool> isTrash { get; set; }
    }
}