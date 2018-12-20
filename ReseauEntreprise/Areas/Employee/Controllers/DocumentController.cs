﻿using Model.Client.Data;
using Model.Client.Service;
using Réseau_d_entreprise.Session;
using ReseauEntreprise.Areas.Employee.Models.ViewModels.Document;
using System.IO;
using System.Web.Mvc;

namespace ReseauEntreprise.Areas.Employee.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Employee/Document
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(UploadForm form) //UploadForm
        {
            if (ModelState.IsValid)
            {
                byte[] file;
                using (Stream stream = form.File.InputStream)
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }

                Document doc = new Document
                {
                    Filename = form.File.FileName,
                    Body = file,
                    AuthorEmployee = SessionUser.GetUser().Id
                };
                DocumentService.Create(doc);
            }
            return View();
        }
        public FileResult Download(int id)
        {
            Document doc = DocumentService.Get(id);
            return File(doc.Body, System.Net.Mime.MediaTypeNames.Application.Octet, doc.Filename);
        }
    }
}