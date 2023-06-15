using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Text.Json;
using WineProject.Models;
using Microsoft.Owin;
using System.Web;
using PagedList;

namespace WineProject.Controllers
{
    public class AdminPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Order CurOrder => GetOrderCookie();
        private IndexWineFiltersModel CurViewModel => GetViewModelCookie();

        private Order GetOrderCookie()
        {
            Order o = new Order();
            if (HttpContext.Request.Cookies.Get("order") != null)
            {
                if (HttpContext.Request.Cookies.Get("order").Value != null && HttpContext.Request.Cookies.Get("order").Value != "")
                {
                    o = JsonSerializer.Deserialize<Order>(HttpContext.Request.Cookies.Get("order").Value);
                }
            }
            return o;
        }

        private IndexWineFiltersModel GetViewModelCookie()
        {
            IndexWineFiltersModel o = new IndexWineFiltersModel();
            if (HttpContext.Request.Cookies.Get("winefiltermodel") != null)
            {
                if (HttpContext.Request.Cookies.Get("winefiltermodel").Value != null && HttpContext.Request.Cookies.Get("winefiltermodel").Value != "")
                {
                    o = JsonSerializer.Deserialize<IndexWineFiltersModel>(HttpContext.Request.Cookies.Get("winefiltermodel").Value);
                }
            }
            return o;
        }

        // GET: AdminPanel
        public ActionResult Index(int? page, int colorId = -1, int brandId = -1, int countryId = -1, int grapeId = -1, int sweetnessId = -1, int typeId = -1)
        {
            IEnumerable<Wine> wines = db.Wines.Include(w => w.Brand).Include(w => w.Color).Include(w => w.Country).Include(w => w.GrapeVariety).Include(w => w.Sweetness).Include(w => w.Type);
            IEnumerable<Color> colors = db.Colors;
            IEnumerable<Brand> brands = db.Brands;
            IEnumerable<Country> countries = db.Countries;
            IEnumerable<GrapeVariety> grapeVarieties = db.GrapeVarieties;
            IEnumerable<Sweetness> sweetnesses = db.Sweetnesses;
            IEnumerable<WineProject.Models.Type> types = db.Types;
            List<Color> colModels = colors
                .Select(d => new Color { Id = d.Id, Name = d.Name })
                .ToList();
            colModels.Insert(0, new Color { Id = 0, Name = "Всі" });
            List<Brand> brandModels = brands
                .Select(d => new Brand { Id = d.Id, Name = d.Name })
                .ToList();
            brandModels.Insert(0, new Brand { Id = 0, Name = "Всі" });
            List<Country> countryModels = countries
                .Select(d => new Country { Id = d.Id, Name = d.Name })
                .ToList();
            countryModels.Insert(0, new Country { Id = 0, Name = "Всі" });
            List<GrapeVariety> gapeModels = grapeVarieties
                .Select(d => new GrapeVariety { Id = d.Id, Name = d.Name })
                .ToList();
            gapeModels.Insert(0, new GrapeVariety { Id = 0, Name = "Всі" });
            List<Sweetness> swModels = sweetnesses
                .Select(d => new Sweetness { Id = d.Id, Name = d.Name })
                .ToList();
            swModels.Insert(0, new Sweetness { Id = 0, Name = "Всі" });
            List<WineProject.Models.Type> typeModels = types
                .Select(d => new WineProject.Models.Type { Id = d.Id, Name = d.Name })
                .ToList();
            typeModels.Insert(0, new WineProject.Models.Type { Id = 0, Name = "Всі" });

            IndexWineViewModel ivm = new IndexWineViewModel { Colors = colModels, Brands = brandModels, Types = typeModels, Sweetnesses = swModels, GrapeVarieties = gapeModels, Countries = countryModels, Wines = wines.ToList() };

            IndexWineFiltersModel ivf = new IndexWineFiltersModel();

            if (colorId == -1 && brandId == -1 && countryId == -1 && grapeId == -1 && sweetnessId == -1 && typeId == -1)
            {
                colorId = CurViewModel.colorId;
                brandId = CurViewModel.brandId;
                countryId = CurViewModel.countryId;
                grapeId = CurViewModel.grapeId;
                sweetnessId = CurViewModel.sweetnessId;
                typeId = CurViewModel.typeId;

            }

            if (colorId == -1)
                colorId = 0;
            if (brandId == -1)
                brandId = 0;
            if (countryId == -1)
                countryId = 0;
            if (grapeId == -1)
                grapeId = 0;
            if (sweetnessId == -1)
                sweetnessId = 0;
            if (typeId == -1)
                typeId = 0;

            if (colorId > 0)
                ivm.Wines = ivm.Wines.Where(w => w.Color.Id == colorId).ToList();
            if (brandId > 0)
                ivm.Wines = ivm.Wines.Where(w => w.Brand.Id == brandId).ToList();
            if (countryId > 0)
                ivm.Wines = ivm.Wines.Where(w => w.Country.Id == countryId).ToList();
            if (grapeId > 0)
                ivm.Wines = ivm.Wines.Where(w => w.GrapeVariety.Id == grapeId).ToList();
            if (sweetnessId > 0)
                ivm.Wines = ivm.Wines.Where(w => w.Sweetness.Id == sweetnessId).ToList();
            if (typeId > 0)
                ivm.Wines = ivm.Wines.Where(w => w.Type.Id == typeId).ToList();

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ivm.PagedWines = ivm.Wines.ToPagedList(pageNumber, pageSize);

            ivf.colorId = colorId;
            ivf.brandId = brandId;
            ivf.countryId = countryId;
            ivf.grapeId = grapeId;
            ivf.sweetnessId = sweetnessId;
            ivf.typeId = typeId;

            string jsonString = JsonSerializer.Serialize(ivf);
            HttpContext.Response.Cookies.Add(new HttpCookie("winefiltermodel", jsonString));
            return View(ivm);
        }

        // GET: AdminPanel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wine = db.Wines.Include(w => w.Brand).Include(w => w.Color).Include(w => w.Country).Include(w => w.GrapeVariety).Include(w => w.Sweetness).Include(w => w.Type);
            Wine ww = wine.Where(w => w.Id == id).ToList()[0];
            if (ww == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = id;
            return View(ww);
        }

        // GET: AdminPanel/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.Id_Brand = new SelectList(db.Brands, "Id", "Name");
            ViewBag.Id_Color = new SelectList(db.Colors, "Id", "Name");
            ViewBag.Id_Country = new SelectList(db.Countries, "Id", "Name");
            ViewBag.Id_GrapeVariety = new SelectList(db.GrapeVarieties, "Id", "Name");
            ViewBag.Id_Sweetness = new SelectList(db.Sweetnesses, "Id", "Name");
            ViewBag.Id_Type = new SelectList(db.Types, "Id", "Name");
            return View();
        }

        // POST: AdminPanel/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,Name,ProductionYear,Potential,Volume,Price,Description,ImageWine,Id_Color,Id_Type,Id_Sweetness,Id_Country,Id_Brand,Id_GrapeVariety")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                db.Wines.Add(wine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Brand = new SelectList(db.Brands, "Id", "Name", wine.Id_Brand);
            ViewBag.Id_Color = new SelectList(db.Colors, "Id", "Name", wine.Id_Color);
            ViewBag.Id_Country = new SelectList(db.Countries, "Id", "Name", wine.Id_Country);
            ViewBag.Id_GrapeVariety = new SelectList(db.GrapeVarieties, "Id", "Name", wine.Id_GrapeVariety);
            ViewBag.Id_Sweetness = new SelectList(db.Sweetnesses, "Id", "Name", wine.Id_Sweetness);
            ViewBag.Id_Type = new SelectList(db.Types, "Id", "Name", wine.Id_Type);
            return View(wine);
        }

        // GET: AdminPanel/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Brand = new SelectList(db.Brands, "Id", "Name", wine.Id_Brand);
            ViewBag.Id_Color = new SelectList(db.Colors, "Id", "Name", wine.Id_Color);
            ViewBag.Id_Country = new SelectList(db.Countries, "Id", "Name", wine.Id_Country);
            ViewBag.Id_GrapeVariety = new SelectList(db.GrapeVarieties, "Id", "Name", wine.Id_GrapeVariety);
            ViewBag.Id_Sweetness = new SelectList(db.Sweetnesses, "Id", "Name", wine.Id_Sweetness);
            ViewBag.Id_Type = new SelectList(db.Types, "Id", "Name", wine.Id_Type);
            return View(wine);
        }

        // POST: AdminPanel/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,ProductionYear,Potential,Volume,Price,Description,ImageWine,Id_Color,Id_Type,Id_Sweetness,Id_Country,Id_Brand,Id_GrapeVariety")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Brand = new SelectList(db.Brands, "Id", "Name", wine.Id_Brand);
            ViewBag.Id_Color = new SelectList(db.Colors, "Id", "Name", wine.Id_Color);
            ViewBag.Id_Country = new SelectList(db.Countries, "Id", "Name", wine.Id_Country);
            ViewBag.Id_GrapeVariety = new SelectList(db.GrapeVarieties, "Id", "Name", wine.Id_GrapeVariety);
            ViewBag.Id_Sweetness = new SelectList(db.Sweetnesses, "Id", "Name", wine.Id_Sweetness);
            ViewBag.Id_Type = new SelectList(db.Types, "Id", "Name", wine.Id_Type);
            return View(wine);
        }

        // GET: AdminPanel/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wine = db.Wines.Include(w => w.Brand).Include(w => w.Color).Include(w => w.Country).Include(w => w.GrapeVariety).Include(w => w.Sweetness).Include(w => w.Type);
            Wine ww = wine.Where(w => w.Id == id).ToList()[0];
            if (ww == null)
            {
                return HttpNotFound();
            }
            return View(ww);
        }

        // POST: AdminPanel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Wine wine = db.Wines.Find(id);
            db.Wines.Remove(wine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private Wine GetWineById(int? id)
        {
            if (id == null)
                return null;
            return db.Wines.Find(id) as Wine;
        }

        private Order AddToCart(int wineId)
        {
            Order o = new Order();
            if (HttpContext.Request.Cookies.Get("order") != null)
            {
                if (HttpContext.Request.Cookies.Get("order").Value == null || HttpContext.Request.Cookies.Get("order").Value == "")
                {
                    o.Sum = 0;
                    o.Total = 0;
                    o.Discount = 0;
                    o.ListGoods = "";
                }
                else
                    o = CurOrder;
            }
            else
                o = CurOrder;
            if (o.ListGoods == "")
                o.ListGoods += wineId.ToString();
            else
                o.ListGoods += ("~" + wineId.ToString());
            o.Sum = 0;
            List<string> result = o.ListGoods.Split('~').ToList();
            foreach (var wid in result)
            {
                if (!wid.Contains("~") && wid.Length != 0)
                {
                    Wine wine = GetWineById(Convert.ToInt32(wid));
                    o.Sum += wine.Price;
                    ViewBag.WineName = wine.Name;
                }
            }
            string jsonString = JsonSerializer.Serialize(o);
            HttpContext.Response.Cookies.Add(new HttpCookie("order", jsonString));
            return o;
        }

        private void MinusOneFromCart(int wineId)
        {
            Order o = new Order();
            if (HttpContext.Request.Cookies.Get("order") != null)
            {
                if (HttpContext.Request.Cookies.Get("order").Value == null || HttpContext.Request.Cookies.Get("order").Value == "")
                {
                    o.Sum = 0;
                    o.Total = 0;
                    o.Discount = 0;
                    o.ListGoods = "";
                }
                else
                    o = CurOrder;
            }
            else
                o = CurOrder;
            if (o.ListGoods == "")
            {
                return;
            }
            else
            {
                o.Sum = 0;
                List<string> result = o.ListGoods.Split('~').ToList();
                int index = result.IndexOf(wineId.ToString());
                if (index >= 0)
                {
                    result.RemoveAt(index);
                    if (result.Count > 0)
                    {
                        if (result[0] == "~")
                            result.RemoveAt(0);
                        else if (result[result.Count - 1] == "~")
                            result.RemoveAt(result.Count - 1);
                        else
                        {
                            for (int i = 1; i < result.Count - 1; i++)
                            {
                                if (result[i] == "~" && result[i - 1] == "~")
                                    result.RemoveAt(i);
                                else if (result[i] == "~" && result[i + 1] == "~")
                                    result.RemoveAt(i);
                            }
                        }
                    }
                }
                o.ListGoods = "";
                foreach (var wid in result)
                {
                    if (!wid.Contains("~") && wid.Length != 0)
                    {
                        o.ListGoods += wid + "~";
                        Wine wine = GetWineById(Convert.ToInt32(wid));
                        o.Sum += wine.Price;
                        ViewBag.WineName = wine.Name;
                    }
                }

                string jsonString = JsonSerializer.Serialize(o);
                HttpContext.Response.Cookies.Add(new HttpCookie("order", jsonString));
            }
        }
        private void DeleteFromCart(int wineId)
        {
            Order o = new Order();
            if (HttpContext.Request.Cookies.Get("order") != null)
            {
                if (HttpContext.Request.Cookies.Get("order").Value == null || HttpContext.Request.Cookies.Get("order").Value == "")
                {
                    o.Sum = 0;
                    o.Total = 0;
                    o.Discount = 0;
                    o.ListGoods = "";
                }
                else
                    o = CurOrder;
            }
            else
                o = CurOrder;
            if (o.ListGoods == "")
            {
                return;
            }
            else
            {
                o.Sum = 0;
                List<string> result = o.ListGoods.Split('~').ToList();
                while (result.Contains(wineId.ToString()))
                {
                    int index = result.IndexOf(wineId.ToString());
                    if (index >= 0)
                    {
                        result.RemoveAt(index);
                        if (result.Count > 0)
                        {
                            if (result[0] == "~")
                                result.RemoveAt(0);
                            else if (result[result.Count - 1] == "~")
                                result.RemoveAt(result.Count - 1);
                            else
                            {
                                for (int i = 1; i < result.Count - 1; i++)
                                {
                                    if (result[i] == "~" && result[i - 1] == "~")
                                        result.RemoveAt(i);
                                    else if (result[i] == "~" && result[i + 1] == "~")
                                        result.RemoveAt(i);
                                }
                            }
                        }
                    }
                }
                o.ListGoods = "";
                foreach (var wid in result)
                {
                    if (!wid.Contains("~") && wid.Length != 0)
                    {
                        o.ListGoods += wid + "~";
                        Wine wine = GetWineById(Convert.ToInt32(wid));
                        o.Sum += wine.Price;
                        ViewBag.WineName = wine.Name;
                    }
                }

                string jsonString = JsonSerializer.Serialize(o);
                HttpContext.Response.Cookies.Add(new HttpCookie("order", jsonString));
            }
        }

        public ActionResult MinusOneCart(int wineId)
        {
            MinusOneFromCart(wineId);
            return Redirect("Cart");
        }

        public ActionResult AddOneCart(int wineId)
        {
            AddToCart(wineId);
            return Redirect("Cart");
            //return AllCart();
        }

        public ActionResult DeleteOneCart(int wineId)
        {
            DeleteFromCart(wineId);
            return Redirect("Cart");
        }

        public ActionResult AddCart(int wineId)
        {
            AddToCart(wineId);
            return Redirect("Index");
        }

        public ActionResult NewOrder(int wineId)
        {
            try
            {
                Order o = new Order();
                if (wineId != -333)
                {
                    o = AddToCart(wineId);
                    o.Discount = db.Users.Where(u => u.UserName == User.Identity.Name).ToList()[0].Discount;
                    ViewBag.WineTotal = o.Sum - (o.Sum * (o.Discount / 100));
                }
                else
                    ViewBag.WineTotal = CurOrder.Sum - (CurOrder.Sum * (db.Users.Where(u => u.UserName == User.Identity.Name).ToList()[0].Discount / 100));
                return View("NewOrder");
            }
            catch
            {
                return Redirect("Index");
            }
        }

        [HttpPost]
        public ActionResult NewOrder(Order @order)
        {
            try
            {
                order.Discount = db.Users.Where(u => u.UserName == User.Identity.Name).ToList()[0].Discount;
                order.ListGoods = CurOrder.ListGoods;
                order.BuyingDay = DateTime.Now;
                order.Sum = CurOrder.Sum;
                order.Total = order.Sum - (order.Sum * (order.Discount / 100));
                order.Id_Customer = db.Users.Where(u => u.UserName == User.Identity.Name).ToList()[0].Id;
                db.Users.Where(u => u.UserName == User.Identity.Name).ToList()[0].Orders.Add(order);
                db.Orders.Add(@order);
                db.SaveChanges();

                if (Request.Cookies["order"] != null)
                {
                    var cookie = new HttpCookie("order")
                    {
                        Expires = DateTime.Now.AddDays(-1d)
                    };
                    Response.Cookies.Add(cookie);
                }
                return RedirectToAction("OrderSuccess");
            }
            catch
            {
                return Redirect("Index");
            }
        }

        [HttpGet]
        public ActionResult Cart()
        {
            Order o;
            if (!HttpContext.Request.Cookies.AllKeys.Contains("order"))
            {
                ViewBag.Amount = 0;
                return View("Cart", new List<CartItem>());
            }
            else
                o = CurOrder;
            List<CartItem> cartList = new List<CartItem>();
            if (o.ListGoods == "")
            {
                ViewBag.Amount = 0;
                return View("Cart", new List<CartItem>());
            }
            else
            {
                List<string> result = new List<string>();
                if (o.ListGoods != null)
                {
                    result = o.ListGoods.Split('~').ToList();
                }
                foreach (var wid in result)
                {
                    if (!wid.Contains("~") && wid.Length != 0)
                    {
                        Wine wine = GetWineById(Convert.ToInt32(wid));
                        bool exist = false;
                        foreach (CartItem ci in cartList)
                        {
                            if (ci.WineId == wine.Id)
                            {
                                ci.Quantity++;
                                ci.Amount += wine.Price;
                                exist = true;
                            }
                        }
                        if (!exist)
                        {
                            CartItem c = new CartItem();
                            var www = db.Wines.Include(w => w.Brand).Include(w => w.Color).Include(w => w.Country).Include(w => w.GrapeVariety).Include(w => w.Sweetness).Include(w => w.Type);
                            int idd = Convert.ToInt32(wid);
                            wine = www.Where(w => w.Id == idd).ToList()[0];
                            c.WineId = wine.Id;
                            c.Price = wine.Price;
                            c.Name = wine.Name;
                            c.Amount = wine.Price;
                            c.Brand = wine.Brand.Name;
                            c.Year = wine.ProductionYear;
                            c.Quantity = 1;
                            cartList.Add(c);
                        }
                    }
                }

                ViewBag.Amount = o.Sum;
                ViewBag.Discount = o.Discount;
                return View("Cart", cartList);
            }
        }

        public ActionResult OrderSuccess()
        {
            return View("OrderSuccess");
        }

        [Authorize(Roles = "admin")]
        public ActionResult OrdersAdmin()
        {
            List<Order> Orders = db.Orders.Include(w => w.Customer).ToList();
            foreach (Order O in Orders)
            {
                string finalIds = "";
                List<string> result = O.ListGoods.Split('~').ToList();
                if (result.Contains(""))
                {
                    while (result.Contains(""))
                    {
                        result.Remove("");
                    }
                }
                while (result.Count != 0)
                {
                    foreach (var wineid in result)
                    {
                        if (!wineid.Contains("~") && wineid.Length != 0)
                        {
                            int count = 0;
                            foreach (var dishid2 in result)
                            {
                                if (!dishid2.Contains("~") && dishid2.Length != 0)
                                {
                                    if (wineid == dishid2)
                                    {
                                        count++;
                                    }
                                }
                            }
                            if (finalIds != "")
                            {
                                finalIds += (" ~ " + wineid);
                                if (count > 1)
                                    finalIds += "x" + count.ToString();
                            }
                            else if (finalIds == "")
                            {
                                finalIds += (wineid);
                                if (count > 1)
                                    finalIds += "x" + count.ToString();
                            }
                            while (result.Contains(wineid.ToString()))
                            {
                                int index = result.IndexOf(wineid.ToString());
                                if (index >= 0)
                                {
                                    result.RemoveAt(index);
                                    //if (result[0] == "~")
                                    //    result.RemoveAt(0);
                                    //else if (result[result.Count - 1] == "~")
                                    //    result.RemoveAt(result.Count - 1);
                                    //else
                                    //{
                                    //    for (int i = 1; i < result.Count - 1; i++)
                                    //    {
                                    //        if (result[i] == "~" && result[i - 1] == "~")
                                    //            result.RemoveAt(i);
                                    //        else if (result[i] == "~" && result[i + 1] == "~")
                                    //            result.RemoveAt(i);
                                    //    }
                                    //}
                                }
                            }
                            break;
                        }
                    }
                }
                O.ListGoods = finalIds;
            }

            return View("OrdersAdmin", Orders);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            ApplicationUser user = db.Users.Find(order.Id_Customer);
            user.Orders.Remove(order);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("OrdersAdmin");
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
