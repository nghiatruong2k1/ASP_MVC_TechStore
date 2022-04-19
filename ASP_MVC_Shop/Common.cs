using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Shop
{
    public class Common
    {
        [NonAction]
        public SelectList getSelectLists(DataTable table,string valueFeild,string textFeild)
        {
            List<SelectListItem> selects = new List<SelectListItem>();
            foreach(DataRow row in table.Rows)
            {
                selects.Add(new SelectListItem()
                {
                    Value=row[valueFeild].ToString(),
                    Text=row[textFeild].ToString()
                });
            }

            return new SelectList(selects, "Value", "Text");
        }
        [NonAction]
        public String getAlias(String str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}