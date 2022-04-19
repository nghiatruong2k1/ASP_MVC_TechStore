using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_MVC_Shop.Master
{
    public class ViewProduct
    {
        public int id { get; set; }

        [Display(Name="Tên")]
        public string name { get; set; }
        [Display(Name="Bí danh")]
        public string alias { get; set; }
        [Display(Name = "Giá")]
        public Nullable<int> price { get; set; }
        [Display(Name = "Khuyến mãi")]
        public Nullable<int> salePrice { get; set; }
        public string currencyUnit { get; set; }
        public Nullable<int> imageId { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string shortDes { get; set; }
        public Nullable<int> typeId { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> createDate { get; set; }
        [Display(Name = "Ngày sửa")]
        public Nullable<System.DateTime> updateDate { get; set; }

        [Display(Name = "Công khai")]
        public Nullable<bool> isPublic { get; set; }
        [Display(Name = "Xóa tạm thời")]
        public Nullable<bool> isTrash { get; set; }
        [Display(Name = "Mô tả đầy đủ")]
        public string fullDes { get; set; }
        public string properties { get; set; }
        [Display(Name = "Danh mục")]
        public string categoryName { get; set; }
        [Display(Name = "Thương hiệu")]
        public string brandName { get; set; }
        [Display(Name = "Hình ảnh")]
        public string imageUrl { get; set; }
        [Display(Name = "Đánh giá")]
        public Nullable<int> rating { get; set; }
        [Display(Name = "Loại")]
        public string typeName { get; set; }
    }
}