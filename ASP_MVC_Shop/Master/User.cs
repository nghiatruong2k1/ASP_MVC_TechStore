using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_Shop.Master
{
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> typeId { get; set; }
        public Nullable<bool> isPublic { get; set; }
        public Nullable<bool> isTrash { get; set; }
        public Nullable<int> imageId { get; set; }
    }
}