using CommonTasks.Data;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.Web.DTOs;
using NetCoreTest.Web.Models;
using NetCoreTest.Web2.Models;
using System.Diagnostics;

namespace NetCoreTest.Web2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var tm = new TestClass { Id = 1, Name = "Test Name" };

            var tDto = new TestDto(); //a quien vamos a llenar

            tm.Transfer(ref tDto);

            return View(tDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
