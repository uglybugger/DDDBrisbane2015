using System;
using System.Web.Mvc;
using DemoWebApp.Core;

namespace DemoWebApp.Controllers
{
    public class DebugController : Controller
    {
        public ActionResult Throw()
        {
            throw new Exception("Test exception").WithReasons("It is *way* too early on Saturday for me to be awake. Go away.");
        }
    }
}