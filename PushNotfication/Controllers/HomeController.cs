using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushNotfication.Models;
using System.Diagnostics;

namespace PushNotfication.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly NotficationDB _notficationDB;

        public HomeController(NotficationDB notficationDB)
        {
            _notficationDB=notficationDB;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
           var token= HttpContext.Request.Cookies["token"];
            return View(_notficationDB.NotificationHistories.Where(x=>x.User.FmcToken==token).AsNoTracking().ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}