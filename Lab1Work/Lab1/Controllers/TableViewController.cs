using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class TableViewController : Controller
    {
        public IActionResult Index()
        {
            
            TableEditModel model = JsonSerializer.Deserialize(TempData["tableModel"].ToString(), typeof(TableEditModel)) as TableEditModel;
            return View(model);
        }
    }
}
