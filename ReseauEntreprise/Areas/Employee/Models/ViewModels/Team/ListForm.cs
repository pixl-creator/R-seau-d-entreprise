﻿using D = Model.Global.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReseauEntreprise.Areas.Employee.Models.ViewModels.Team
{
    public class ListForm
    {
        [Key]
        public int TeamId { get; set; }
        public String Name { get; set; }
        public DateTime CreationDate { get; set; }
        public D.Employee TeamLeader { get; set; }
        public D.Employee Creator { get; set; }
        public D.Project Project { get; set; }
        public DateTime? EndDate { get; set; }
        public bool AmIPartOfTeam { get; set; }
        public bool AmITeamLeader { get; set; }
        public bool AmIProductManager { get; set; }
        
    }
}