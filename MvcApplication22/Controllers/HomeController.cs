using PeopleSearch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication22.Models;


namespace MvcApplication22.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string lastName)
        {
            PeopleManager manager = new PeopleManager(Properties.Settings.Default.ConStr);
            IEnumerable<Person> people;
            if (String.IsNullOrEmpty(lastName))
            {
                people = manager.GetAll();
            }
            else
            {
                people = manager.SearchByLastName(lastName);
            }
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.People = people;
            viewModel.LastName = lastName;
            return View(viewModel);
        }

        public ActionResult Stuff()
        {
            StuffViewModel viewModel = new StuffViewModel();
            if (Request.Cookies["beenhere"] == null)
            {
                viewModel.BeenHere = false;
                Response.Cookies.Add(new HttpCookie("beenhere", DateTime.Now.ToShortTimeString()));
            }
            else
            {
                HttpCookie cookie = Request.Cookies["beenhere"];
                viewModel.FirstTime = cookie.Value;
                viewModel.BeenHere = true;

            }
            return View(viewModel);
        }

        public void Increment()
        {
            int num = 0;
            if (Request.Cookies["num"] == null)
            {
                num = 1;
            }
            else
            {
                num = int.Parse(Request.Cookies["num"].Value) + 1;
            }

            Response.Cookies.Add(new HttpCookie("num", num.ToString()));

            Response.Write("<h1>" + num + "</h1>");

        }



    }
}
