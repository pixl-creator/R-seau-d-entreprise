﻿using G = Model.Global.Data;
using Model.Global.Service;
using Réseau_d_entreprise.Session.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Réseau_d_entreprise.Session;
using Model.Global.Data;
using ReseauEntreprise.Areas.Employee.Models.ViewModels.Project;

namespace ReseauEntreprise.Employee.Controllers
{
    [RouteArea("Employee")]
    [EmployeeRequired]
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            List<ListForm> list = new List<ListForm>();
            foreach (Project Project in ProjectService.GetAllActive())
            {
                int? ManagerId = ProjectService.GetProjectManagerId(Project.Id);
                G.Employee Manager = EmployeeService.Get((int)ManagerId);
                G.Employee Creator = EmployeeService.Get(Project.CreatorId);
                ListForm form = new ListForm(Project, Manager, Creator);
                list.Add(form);
            }
            return View(list);
        }
        public ActionResult History()
        {
            return View();
        }

        


        public ActionResult Edit(int id)
        {
            Project project = ProjectService.GetProjectById(id);
            G.Employee Manager = EmployeeService.Get((int)ProjectService.GetProjectManagerId(id));
            EditForm form = new EditForm()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ProjectManagerId = Manager.Employee_Id,
                SelectedProjectManagerId = Manager.Employee_Id,
                StartDate = project.Start,
                EndDate = project.End,
                CreatorId = project.CreatorId
            };
            IEnumerable<G.Employee> Employees = EmployeeService.GetAllActive();
            List<SelectListItem> ManagerCandidates = new List<SelectListItem>();
            foreach (G.Employee emp in Employees)
            {
                ManagerCandidates.Add(new SelectListItem()
                {
                    Text = emp.FirstName + " " + emp.LastName + " (" + emp.Email + ")",
                    Value = emp.Employee_Id.ToString()
                });
            }
            if (!Employees.Any(emp => emp.Employee_Id == Manager.Employee_Id))
            {
                ManagerCandidates.Add(new SelectListItem()
                {
                    Text = "!!!VIRÉ!!! " + Manager.FirstName + " " + Manager.LastName + " (" + Manager.Email + ")",
                    Value = Manager.Employee_Id.ToString()
                });
            }
            form.ProjectManagerCandidateList = ManagerCandidates;

            return View(form);
        }

        [HttpPost]
        public ActionResult Edit(EditForm form)
        {
            if (ModelState.IsValid)
            {
                G.Project Project = new G.Project()
                {
                    Id = form.Id,
                    Name = form.Name,
                    Description = form.Description,
                    Start = form.StartDate,
                    End = form.EndDate,
                    CreatorId = form.CreatorId,
                    ProjectManagerId = form.SelectedProjectManagerId
                };
                try
                {
                   if (ProjectService.Edit(SessionUser.GetUser().Id, Project))
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (System.Data.SqlClient.SqlException exception)
                {
                    throw (exception);
                }
                return RedirectToAction("Edit");
            }
            return View(form);
        }
        

        public ActionResult Details(int id)
        {

            G.Project Project = ProjectService.GetProjectById(id);
            G.Employee Manager = EmployeeService.Get((int)ProjectService.GetProjectManagerId(id));
            G.Employee Creator = EmployeeService.Get(Project.CreatorId);
            IEnumerable<G.Team> Teams = ProjectService.GetAllTeamsForProject(id);
            DetailsForm Form = new DetailsForm
            {
                Id = Project.Id,
                Name = Project.Name,
                Description = Project.Description,
                Manager = Manager,
                Creator = Creator,
                StartDate = Project.Start,
                EndDate = Project.End,
                Teams = Teams
            };

            return View(Form);
        }
    }
}