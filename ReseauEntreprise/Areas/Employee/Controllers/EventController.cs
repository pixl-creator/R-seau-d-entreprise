﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C = Model.Client.Data;
using Model.Client.Service;
using Réseau_d_entreprise.Session;
using ReseauEntreprise.Areas.Admin.Models.ViewModels.Event;
using Réseau_d_entreprise.Session.Attributes;

namespace ReseauEntreprise.Areas.Employee.Controllers
{
    [RouteArea("Employee")]
    [EmployeeRequired]
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<ListForm> Events = EventService.GetAllActiveForUser(SessionUser.GetUser().Id)
                .Select(e => new ListForm()
                {
                    Id = (int) e.Id,
                    DepartmentId = e.DepartmentId,
                    Name = e.Name,
                    Description = e.Description,
                    Address = e.Address,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    OpenSubscription = e.Open,
                    Subscribed = e.Subscribed
                });

            return View(Events);
        }

        public ActionResult Details(int id)
        {
            C.Event Event = EventService.Get(id);
            DetailsForm form = new DetailsForm
            {
                Id = (int)Event.Id,
                CreatorId = Event.CreatorId,
                Name = Event.Name,
                Description = Event.Description,
                Address = Event.Address,
                StartDate = Event.StartDate,
                EndDate = Event.EndDate,
                CreationDate = Event.CreationDate
            };
            return View(form);
        }

        public ActionResult SubscribeEmployees(int id)
        {
            IEnumerable<C.EmployeeEvent> EmployeesStatus = EventService.GetSubscriptionStatus(id);
            IEnumerable<C.Employee> EmployeeList = EmployeeService.GetAllActive();

            List<EmployeeSelectorForm> EmployeesForm = new List<EmployeeSelectorForm>();
            foreach (C.Employee e in EmployeeList)
            {
                EmployeesForm.Add(new EmployeeSelectorForm
                {
                    Employee = e,
                    EmployeeId = (int)e.Employee_Id,
                    EventId = id,
                    Selected = EmployeesStatus.Where((employee) => employee.EmployeeId == e.Employee_Id).Count() ==1
                });
            }
            return View(EmployeesForm);
        }

        [HttpPost]
        public ActionResult SubscribeEmployees(IEnumerable<EmployeeSelectorForm> Form)
        {
            foreach (EmployeeSelectorForm item in Form)
            {
                if (item.Selected)
                {
                    EventService.Participate(item.EventId, item.EmployeeId);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmSubscription(int id)
        {
            C.Event Event = EventService.Get(id);
            DetailsForm Form = new DetailsForm
            {
                Id = (int)Event.Id,
                CreatorId = Event.CreatorId,
                DepartmentId = Event.DepartmentId,
                Name = Event.Name,
                Description = Event.Description,
                Address = Event.Address,
                StartDate = Event.StartDate,
                EndDate = Event.EndDate,
                OpenSubscription = Event.Open,
                CreationDate = Event.CreationDate
            };

            return View(Form);
        }

        [HttpPost]
        public ActionResult ConfirmSubscription(DetailsForm Form)
        {

            EventService.Participate(Form.Id, SessionUser.GetUser().Id);
            return RedirectToAction("Index");
        }

        public ActionResult Attendance(int id)
        {
            EventService.Get(id);
            return View();
        }

        public ActionResult CancelSubscription(int id)
        {
            EventService.Get(id);
            return View();
        }
    }
}