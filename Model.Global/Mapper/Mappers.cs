﻿using Model.Global.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Global.Mapper
{
    internal static class Mappers
    {
        internal static Employee ToEmployee(this IDataRecord dr)
        {
            Employee e = new Employee() {
                Employee_Id = (int)dr["Employee_Id"],
                LastName = (string)dr["LastName"],
                FirstName = (string)dr["FirstName"],
                Email = (string)dr["Email"],
                RegNat = (string)dr["RegNat"],
                Address = (string)dr["Address"],
                Phone = (dr["Phone"] == DBNull.Value) ? null : (string)dr["Phone"]
            };
            if (Enumerable.Range(0, dr.FieldCount).Select(x => dr.GetName(x)).Contains("Actif"))
            {
                e.IsAdmin = (dr["Actif"] == DBNull.Value) ? false : (bool)dr["Actif"];
            }
            return e;
        }
        internal static Project ToProject(this IDataRecord dr)
        {
            return new Project()
            {
                Id = (int)dr["Project_Id"],
                Name = (string)dr["Project_Name"],
                Description = (string)dr["Project_Description"],
                Start = (DateTime)dr["StartDate"],
                End =(DateTime?)  ((dr["EndDate"] == DBNull.Value) ? null : dr["EndDate"]),
                CreatorId = (int)dr["CreatorId"]
            };
        }
    }
}
