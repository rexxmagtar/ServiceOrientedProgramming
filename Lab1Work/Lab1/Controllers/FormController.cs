using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class FormController : Controller
    {
        [HttpPost]
        public ActionResult Index(int tableFieldsNumber)
        {
            HttpContext.Session.SetInt32("tableFieldsNumber",tableFieldsNumber);
            TempData["tableFieldsNumber"] = tableFieldsNumber;
            return RedirectToAction("Index", "CreateTable",tableFieldsNumber);
        }
    }
}
