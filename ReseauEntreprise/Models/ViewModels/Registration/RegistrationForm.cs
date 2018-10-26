﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Réseau_d_entreprise.Models.ViewModels
{
    public class RegistrationForm
    {
        [Required]
        [MaxLength(50)]
        [DisplayName("Nom")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(360)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [DisplayName("Mot de passe")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [DisplayName("Retapez le mot de passe")]
        public String Confirm { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Numéro de registre national")]
        public string RegNat { get; set; }
        [Required]
        [DisplayName("Adresse")]
        public string Address { get; set; }
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Numéro de téléphone")]
        public string Phone { get; set; }
    }
}