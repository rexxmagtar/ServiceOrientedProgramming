
using System.Text.Json;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;

namespace Lab1.Controllers
{
    public class CreateTableController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            TableCreateModel tableCreateModel = new TableCreateModel();

            int numberOfFields = int.Parse(TempData["tableFieldsNumber"].ToString());
            TempData.Save();

            for (int i = 0; i < numberOfFields; i++)
            {
                tableCreateModel.Fields.Add(new TableCreateModel.Field($"field_{i}",TableCreateModel.allowedDataType.text,false));
            }

            return View("CreateTablePage",tableCreateModel);
        }

        [HttpPost]
        public IActionResult CreateTable(TableCreateModel model,int primaryField)
        {
            model.Fields[primaryField].IsPrimaryKey = true;

            string cs = @"Data source=C:\BSU_Season5\ServiceOrientedProgramming\Lab1Work\Lab1\DB\data.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = con.CreateCommand();

            string fieldsStr = "";

            for (int i = 0; i < model.Fields.Count; i++)
            {
                fieldsStr += model.Fields[i].FieldName + " " + GetDbType(model.Fields[i].FieldType);
                
                if (model.Fields[i].IsPrimaryKey)
                {
                    fieldsStr += " PRIMARY KEY";
                }

                if (i < model.Fields.Count - 1)
                {
                    fieldsStr += ", ";
                }
            }

            cmd.CommandText = "DROP TABLE IF EXISTS newTable";

            cmd.ExecuteNonQuery();

            cmd.CommandText = $"CREATE TABLE IF NOT EXISTS newTable ({fieldsStr})";

            cmd.VerifyOnly();

            cmd.ExecuteNonQuery();

            TempData["TableCreateModel"] =JsonSerializer.Serialize(model,typeof(TableCreateModel));
            return RedirectToAction("Index", "EditTable");
        }

        public static string GetDbType(TableCreateModel.allowedDataType type)
        {
            switch (type)
            {
                case TableCreateModel.allowedDataType.floatNumber:
                {
                    return "REAL";
                }
                case TableCreateModel.allowedDataType.intergerNumber:
                {
                    return "INTEGER";
                }
                default:
                {
                    return "TEXT";
                }
            }
        }
    }
}
