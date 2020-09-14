using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Models
{
    public class TableCreateModel
    {
        public List<SelectListItem> types { get; } = new List<SelectListItem>()
        {
            new SelectListItem("text", DbTableModel.allowedDataType.text.ToString()),
            new SelectListItem("integer number",  DbTableModel.allowedDataType.intergerNumber.ToString()),
            new SelectListItem("float number", DbTableModel.allowedDataType.floatNumber.ToString()),
            new SelectListItem("boolean", DbTableModel.allowedDataType.boolean.ToString())
        };

        public List<DbTableModel.FieldInfo> Fields { get; set; } = new List<DbTableModel.FieldInfo>();
    }
}
