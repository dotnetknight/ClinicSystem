using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicSakurso.Models
{
    [MetadataType(typeof(DoctorMetaData))]
    public partial class Doctor { }

    public class DoctorMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "სახელი სავალდებულოა")]
        public string First_Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "გვარი სავალდებულოა")]
        public string Last_Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "განყოფილება სავალდებულოა")]
        public string Department { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ოთახი სავალდებულოა")]
        public string Room { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ტელეფონი სავალდებულოა")]
        public string Phone { get; set; }
    }
}