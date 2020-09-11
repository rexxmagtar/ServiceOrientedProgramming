using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Models
{
    public class TableCreateModel
    {
        public enum allowedDataType
        {
            intergerNumber,
            floatNumber,
            text,
            boolean
        }

        public class Field
        {
            public string FieldName { get; set; }
            public allowedDataType FieldType { get; set; }
            public bool IsPrimaryKey { get; set; }
        }

        public List<SelectListItem> types { get; } = new List<SelectListItem>()
        {
            new SelectListItem("text", allowedDataType.text.ToString()),
            new SelectListItem("integer number",  allowedDataType.intergerNumber.ToString()),
            new SelectListItem("float number", allowedDataType.floatNumber.ToString()),
            new SelectListItem("boolean", allowedDataType.boolean.ToString())
        };

        public List<Field> Fields { get; set; } = new List<Field>();
    }
}
