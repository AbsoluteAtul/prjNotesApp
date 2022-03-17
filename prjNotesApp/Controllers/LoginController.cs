using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjNotesApp.Models;
using System.Data.SqlClient; //this namespace is for sqlclient server  
using System.Configuration; // this namespace is add I am adding connection name in web config file config connection name 

namespace prjNotesApp.Controllers
{
    public class LoginController : Controller
    {
       
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.RetunUrl = Request.QueryString["ReturnUrl"];
            UserProfile user = new UserProfile(); 
            return View(user);
        }

        [HttpPost]
        public ActionResult Index(UserProfile user)
        {
          
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbNote2"].ToString());
            try
            {

                    con.Open();
                    string qry = "select * from tabLogin where username='" + user.Username + "' and password='" + user.Password + "'";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        // next time the same person come cookie will identify him
                        HttpCookie cookie = new HttpCookie("AuthCookie");
                        cookie.Value = user.Username;
                        if (user.RememberMe)
                        {
                            // if user checked remember me cookie should be persistent
                            cookie.Expires = DateTime.Now.AddDays(7);
                        }
                        cookie.Path = Request.ApplicationPath;
                        Response.Cookies.Add(cookie);

                        //if the returnurl is there it will show the same page after authentication or it will go to the index
                        string return_url = Request.QueryString["ReturnUrl"];
                        if (string.IsNullOrEmpty(return_url))
                        {
                            return Redirect("/");
                        }
                        else
                        {
                            return Redirect(return_url);
                        }
                        // redirect to the Index

                    }
                    else
                    {
                        ViewBag.Error = "Invalid UserName or Password";
                    }
                }
                catch (Exception e)
                {
                    Response.Write(e.Message);
                }

            con.Close();
            return View(user);
        }

        //private void Authenticate(string returnUrl)
        //{
        //    HttpCookie cookie = Request.Cookies["AuthCookie"];
        //    //if the user didnt logged in the cookie will be null 
        //    if(cookie == null)
        //    {
        //        Response.Redirect("/Login/Index?ReturnUrl="+returnUrl);
        //    }
        //}

        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["AuthCookie"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
            cookie.Path = Request.ApplicationPath;
            Response.Cookies.Add(cookie);
            return Redirect("/Login/Index");
        }
        //public ActionResult Practice(UserProfile user)
        //{
        //    Authenticate("/Login/Practice");
        //    return View(user);
        //}
    }

}