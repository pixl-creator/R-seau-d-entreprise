﻿using Model.Client.Data;
using Model.Client.Mapper;
using GS = Model.Global.Service;
using GD = Model.Global.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Client.Service
{
    public static class EmployeeService
    {

        public static IEnumerable<Employee> GetAllActive()
        {
            List<Employee> ClientEmployees = new List<Employee>();
            IEnumerable<GD.Employee> GlobalEmployees = GS.EmployeeService.GetAllActive();
            foreach (GD.Employee employee in GlobalEmployees)
            {
                ClientEmployees.Add(Mappers.ToClient(employee));
            }
            return ClientEmployees;
        }

        public static Employee Get(int Id)
        {
            return Mappers.ToClient(GS.EmployeeService.Get(Id));
        }

        public static bool Update(Employee e)
        {
            return GS.EmployeeService.Update(Mappers.ToGlobal(e));
        }

        public static bool UpdateEmail(Employee e)
        {
            return GS.EmployeeService.UpdateEmail(Mappers.ToGlobal(e));
        }

        public static bool UpdatePassword(Employee e, string OldPass)
        {
            return GS.EmployeeService.UpdatePassword(Mappers.ToGlobal(e), OldPass);
        }

        public static bool Delete(Employee e)
        {
            return GS.EmployeeService.Delete(Mappers.ToGlobal(e));
        }

        public static Employee GetForAdmin(int Id)
        {
            return Mappers.ToClient(GS.EmployeeService.GetForAdmin(Id));
        }

        public static IEnumerable<Employee> GetAllActiveForAdmin()
        {
            List<Employee> ClientEmployees = new List<Employee>();
            IEnumerable<GD.Employee> GlobalEmployees = GS.EmployeeService.GetAllActiveForAdmin();
            foreach (GD.Employee employee in GlobalEmployees)
            {
                ClientEmployees.Add(Mappers.ToClient(employee));
            }
            return ClientEmployees;
        }

        public static bool UpdateForAdmin(Employee e)
        {

            return GS.EmployeeService.UpdateForAdmin(Mappers.ToGlobal(e));
        }

        public static IEnumerable<EmployeeStatusHistory> GetEmployeeStatusHistory(int Employee_Id)
        {
            List<EmployeeStatusHistory> ClientEmployeeStatusHistory = new List<EmployeeStatusHistory>();
            IEnumerable<GD.EmployeeStatusHistory> GlobalEmployeeStatusHistory = GS.EmployeeService.GetEmployeeStatusHistory(Employee_Id);
            foreach (GD.EmployeeStatusHistory employeeStatusHistory in GlobalEmployeeStatusHistory)
            {
                ClientEmployeeStatusHistory.Add(Mappers.ToClient(employeeStatusHistory));
            }
            return ClientEmployeeStatusHistory;
        }
        public static IEnumerable<EmployeeProjectManagerHistory> GetEmployeeProjectManagerHistory(int Employee_Id)
        {
            List<EmployeeProjectManagerHistory> ClientEmployeeProjectManagerHistory = new List<EmployeeProjectManagerHistory>();
            IEnumerable<GD.EmployeeProjectManagerHistory> GlobalEmployeeProjectManagerHistory = GS.EmployeeService.GetEmployeeProjectManagerHistory(Employee_Id);
            foreach (GD.EmployeeProjectManagerHistory employeeProjectManagerHistory in GlobalEmployeeProjectManagerHistory)
            {
                ClientEmployeeProjectManagerHistory.Add(Mappers.ToClient(employeeProjectManagerHistory));
            }
            return ClientEmployeeProjectManagerHistory;
        }

    }
}
