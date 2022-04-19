using ASP_MVC_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_Shop.Contexts;
namespace ASP_MVC_Shop.Controllers
{
    public class UserController : BaseController
    {
        [Route("dang-ky")]
        public ActionResult Register()
        {
            return View(new AuthRegisterModel());
        }
        [Route("quen-mat-khau")]
        public ActionResult Forget()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [Route("dang-ky")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AuthRegisterModel _user)
        {
            if (ModelState.IsValid)
            {
                string password = _user.password;
                _user.password = GetMD5(_user.password);
                var check = objEntity.User_0256.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    User_0256 user = new User_0256();
                    user.email = _user.email;
                    user.password = _user.password;
                    user.firstName = _user.firstName;
                    user.lastName = _user.lastName;
                    user.typeId = 1;
                    objEntity.Configuration.ValidateOnSaveEnabled = false;
                    objEntity.User_0256.Add(user);
                    objEntity.SaveChanges();

                    lstToast.Add(new Toast() { message = "Đăng ký thành công!", type = "success" });
                    EndAction();
                    return Login(new AuthLoginModel() { email = _user.email, password = password });
                }
                else
                {
                    lstToast.Add(new Toast() { message = "Email đã tồn tại!", type = "warning" });

                }
            }
            lstToast.Add(new Toast() { message = "Đăng ký không thành công!", type = "warning" });
            EndAction();
            return View(_user);


        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [Route("dang-nhap")]
        public ActionResult Login()
        {
            return View(new AuthLoginModel());
        }

        [HttpPost]
        [Route("dang-nhap")]
        [ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AuthLoginModel auth)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(auth.password);
                var data = objEntity.User_0256.Where(s => s.email.Equals(auth.email) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["fullName"] = data.FirstOrDefault().firstName + " " + data.FirstOrDefault().lastName;
                    Session["email"] = data.FirstOrDefault().email;
                    Session["id"] = data.FirstOrDefault().id;
                    Session["typeId"] = data.FirstOrDefault().typeId;

                    lstToast.Add(new Toast() { message = "Đăng nhập thành công!", type = "success" });                  
                    if(data.FirstOrDefault().typeId == 4)
                    {
                        lstToast.Add(new Toast() { message = "Chào mừng quản trị viên " + Session["fullName"] });
                    }
                    else
                    {
                        lstToast.Add(new Toast() { message = "Chào mừng " + Session["fullName"] });
                    }
                    EndAction();
                    return Redirect("/");
                }
                else
                {
                    lstToast.Add(new Toast() { message = "Email hoặc mật khẩu không chính xác!", type = "warning" });
                }
            }
            lstToast.Add(new Toast() { message = "Đăng nhập không thành công!", type = "warning" });
            EndAction();
            return View(auth);
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            lstToast.Add(new Toast() { message = "Đăng xuất thành công!", type = "success" });
            EndAction();
            return RedirectToAction("Login");
        }
    }
}