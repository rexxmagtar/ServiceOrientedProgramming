using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Lab1.Models;

namespace Lab1
{
    public class SqliteDbConverter
    {

        public static string GetDbType(DbTableModel.allowedDataType type)
        {
            switch (type)
            {
                case DbTableModel.allowedDataType.floatNumber:
                {
                    return "REAL";
                }
                case DbTableModel.allowedDataType.intergerNumber:
                {
                    return "INTEGER";
                }
                case DbTableModel.allowedDataType.boolean:
                {
                    return "BOOLEAN";
                }
                default:
                {
                    return "TEXT";
                }
            }
        }

        public static DbTableModel.allowedDataType GetTypeFromString(string type)
        {
            switch (type)
            {
                case "REAL":
                {
                    return DbTableModel.allowedDataType.floatNumber;
                }
                case "INTEGER":
                {
                    return DbTableModel.allowedDataType.intergerNumber;
                }
                case "BOOLEAN":
                {
                    return DbTableModel.allowedDataType.boolean;
                }
                default:
                {
                    return DbTableModel.allowedDataType.text;
                }
            }
        }


        public static DbTableModel GetModel(string connectionStr, string tableName)
        {
            using var con = new SQLiteConnection(connectionStr);
            con.Open();

            using var cmd = con.CreateCommand();

            cmd.CommandText = $"SELECT * FROM {tableName}";


            cmd.VerifyOnly();

            SQLiteDataReader reader = cmd.ExecuteReader();

            DbTableModel model = new DbTableModel();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                model.FieldsInfo.Add(new DbTableModel.FieldInfo(reader.GetName(i), SqliteDbConverter.GetTypeFromString(reader.GetDataTypeName(i)), false));
            }

            while (reader.Read())
            {
                model.Table.Add(new List<string>());

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (model.FieldsInfo[i].FieldType == DbTableModel.allowedDataType.floatNumber)
                    {
                        model.Table[model.Table.Count - 1].Add(reader.GetFloat(i).ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        model.Table[model.Table.Count - 1].Add(reader.GetValue(i).ToString());
                    }
                }
            }

            return model;
        }
    }
}
