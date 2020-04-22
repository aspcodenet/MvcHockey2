using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc2Hockey.Models;

namespace Mvc2Hockey.ViewModels
{
    public class PlayerListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonNummerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string s = value.ToString();
            if (s.Length == 10)

                if (s.Length == 12 || s.Length == 10)
                    return ValidationResult.Success;
            return new ValidationResult("Inget giltigt personnumer");
        }
    }

    public class PlayerViewModel
    {
        public int DbNumber { get; set; }


        [Required] public string Name { get; set; }

        [PersonNummer] public string PersonNummer { get; set; }

        //[Remote("CheckJersey","Player")]
        public int JerseyNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime FirstNhlGame { get; set; }

        [Range(1, 100)] public int Age { get; set; }


    }


    public class PlayerEnterValgorenhetModel : IValidatableObject
    {
        public int DbNumber { get; set; }


        public int Saldo { get; set; } //1000
        public int Vg1 { get; set; } // 500
        public int Vg2 { get; set; } // 600

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DbNumber == 1)
            {
                
            }
            else if(Vg1 + Vg2 > Saldo)
            {
                yield return new ValidationResult("Saldo räcker inte");
            }
        }
    }

}
