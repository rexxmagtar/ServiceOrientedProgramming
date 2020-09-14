using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    public class EditTableController : Controller
    {
        public IActionResult Index()
        {

            TableCreateModel model = JsonSerializer.Deserialize(TempData["TableCreateModel"].ToString(), typeof(TableCreateModel)) as TableCreateModel;
            DbTableModel dbTableModel = new DbTableModel();

            dbTableModel.FieldsInfo = new List<DbTableModel.FieldInfo>(model.Fields);

            if (dbTableModel.Table.Count == 0)
            {
                dbTableModel.Table.Add(new List<string>());
                for (int i = 0; i < dbTableModel.FieldsInfo.Count; i++)
                {
                    if (dbTableModel.FieldsInfo[i].FieldType == DbTableModel.allowedDataType.boolean)
                    {
                        dbTableModel.Table[0].Add("False");
                    }
                    else
                    {
                        dbTableModel.Table[0].Add("");
                    }
                }
            }

            return View(dbTableModel);
        }

        public IActionResult EditReadyModel(DbTableModel model)
        {
            return View("Index", model);
        }

        public IActionResult EditSavedTable()
        {
            DbTableModel model = SqliteDbConverter.GetModel(@"Data source=C:\BSU_Season5\ServiceOrientedProgramming\Lab1Work\Lab1\DB\data.db", "newTable");

            return EditReadyModel(model);
        }

        public IActionResult AddRowToModel(DbTableModel model)
        {
            model.Table.Add(new List<string>());
            for (int i = 0; i < model.FieldsInfo.Count; i++)
            {
                if (model.FieldsInfo[i].FieldType == DbTableModel.allowedDataType.boolean)
                {
                    model.Table[model.Table.Count - 1].Add("False");
                }
                else
                {
                    model.Table[model.Table.Count - 1].Add("");
                }
            }
            return View("Index", model);
        }

        public IActionResult FinishEditing(DbTableModel model)
        {
            string cs = @"Data source=C:\BSU_Season5\ServiceOrientedProgramming\Lab1Work\Lab1\DB\data.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = con.CreateCommand();

            string valuesStr = "";

            string fieldsNames = "";

            cmd.CommandText = $"DELETE FROM newTable";

            cmd.ExecuteNonQuery();

            for (int i = 0; i < model.FieldsInfo.Count; i++)
            {
                fieldsNames += " " + model.FieldsInfo[i].FieldName;

                if (i < model.FieldsInfo.Count - 1)
                {
                    fieldsNames += ",";
                }
            }

            for (int i = 0; i < model.Table.Count; i++)
            {
                valuesStr += "(";

                for (int j = 0; j < model.Table[i].Count; j++)
                {
                    valuesStr += " '" + model.Table[i][j] + "'";

                    if (j < model.Table[i].Count - 1)
                    {
                        valuesStr += ",";
                    }
                }
                valuesStr += ")";

                if (i < model.Table.Count - 1)
                {
                    valuesStr += ",";
                }
            }

            

            cmd.CommandText = $"INSERT INTO newTable ({fieldsNames}) VALUES {valuesStr}";

            cmd.VerifyOnly();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                return Content(exception.ToString());
            }

            TempData["tableModel"] = JsonSerializer.Serialize(model, typeof(DbTableModel));
            return RedirectToAction("Index", "TableView");
        }


    }
}

