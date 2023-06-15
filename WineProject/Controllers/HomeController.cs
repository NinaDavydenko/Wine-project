using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WineProject.Controllers
{
    public class HomeController : Controller
    {
      //  <add name = "DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-WineProject-20220125025332.mdf;Initial Catalog=aspnet-WineProject-20220125025332;Integrated Security=True"
      //providerName="System.Data.SqlClient" />
        public ActionResult Index()
        {
            return this.RedirectToAction("Index", "AdminPanel");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}