using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Models
{
    public class TableEditModel
    {
        public List<List<string>> Table { get; set; } = new List<List<string>>();
        public List<TableCreateModel.Field> FieldsInfo { get; set; } = new List<TableCreateModel.Field>();

        public TableEditModel(TableCreateModel createModel)
        {
            FieldsInfo = new List<TableCreateModel.Field>(createModel.Fields);
        }

        public TableEditModel()
        {
            //FieldsInfo.Add(new TableCreateModel.Field());
        }

        //public static string GetHtmlString(TableCreateModel.allowedDataType savedType)
        //{
        //    switch (savedType)
        //    {
             
        //    }
        //}
    }
}
