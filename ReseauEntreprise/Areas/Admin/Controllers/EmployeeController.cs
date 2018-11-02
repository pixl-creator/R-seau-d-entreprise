﻿using D=Model.Global.Data;
using Model.Global.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReseauEntreprise.Areas.Admin.Models.ViewModels.Employee;
using Réseau_d_entreprise.Session.Attributes;

namespace ReseauEntreprise.Admin.Controllers
{
    [AdminRequired]
    public class EmployeeController : Controller
    {
        // GET: Admin/Employee
        public ActionResult Index()
        {
            IEnumerable<EmployeeList> AllEmployees = EmployeeService.GetAllActiveForAdmin()
                .Select(e => new EmployeeList()
                {
                    Id = e.Employee_Id,
                    LastName = e.LastName,
                    FirstName = e.FirstName,
                    Email = e.Email,
                    Address = e.Address,
                    Phone = e.Phone,
                    RegNat = e.RegNat,
                    IsAdmin = e.IsAdmin
                });
            return View(AllEmployees);
        }

        public ActionResult Delete(int id)
        {
            D.Employee emp = EmployeeService.Get(id);
            EmployeeList e = new EmployeeList()
            {
                LastName = emp.LastName,
                FirstName = emp.FirstName,
                Email = emp.Email,
                Address = emp.Address,
                Phone = emp.Phone,
                RegNat = emp.RegNat
            };
            return View(e);
        }

        [HttpPost]
        public ActionResult Delete(int id, EmployeeList HiddenForm)
        {
            try
            {
                if (HiddenForm.Id == id)
                {
                    D.Employee e = new D.Employee()
                    {
                        Employee_Id = HiddenForm.Id,
                        LastName = HiddenForm.LastName,
                        FirstName = HiddenForm.FirstName,
                        Email = HiddenForm.Email,
                        Address = HiddenForm.Address,
                        Phone = HiddenForm.Phone,
                        RegNat = HiddenForm.RegNat
                    };
                    if (EmployeeService.Delete(e))
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception exception)
            {
                throw (exception);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                D.Employee e = EmployeeService.GetForAdmin(id);
                if (!(e is null))
                {
                    EmployeeEditForm form = new EmployeeEditForm()
                    {
                        Id = id,
                        LastName = e.LastName,
                        FirstName = e.FirstName,
                        Email = e.Email,
                        Address = e.Address,
                        Phone = e.Phone,
                        RegNat = e.RegNat,
                        IsAdmin = e.IsAdmin
                    };
                    return View(form);
                }
            }
            catch (Exception exception)
            {
                throw (exception);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(int id, EmployeeEditForm form)
        {
            if (ModelState.IsValid)
            {
                if (id == form.Id)
                {
                    D.Employee e = new D.Employee()
                    {
                        Employee_Id = id,
                        LastName = form.LastName,
                        FirstName = form.FirstName,
                        Email = form.Email,
                        Passwd = form.Password,
                        Address = form.Address,
                        Phone = form.Phone,
                        RegNat = form.RegNat,
                        IsAdmin = form.IsAdmin
                    };
                    try
                    {
                        if (EmployeeService.UpdateForAdmin(e))
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch (System.Data.SqlClient.SqlException exeption)
                    {
                        if (exeption.Number == 2627)
                        {
                            if (exeption.Message.Contains("UC_Email"))
                            {
                                ModelState.AddModelError("Email", "Cet email est déjà utilisé");
                            }
                            if (exeption.Message.Contains("UC_RegNat"))
                            {
                                ModelState.AddModelError("RegNat", "Ce numero de régistre national est déjà utilisé.");
                            }
                        }
                    }
                }
            }
            return View(form);
        }
    }
}