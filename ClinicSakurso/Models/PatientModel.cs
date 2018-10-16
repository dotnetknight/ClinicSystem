using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicSakurso.Models
{
    public class PatientModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "სახელი სავალდებულოა")]
        public string First_Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "გვარი სავალდებულოა")]
        public string Last_Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "დაბ.თარიღი სავალდებულოა")]
        public string Birth_Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "მობილური სავალდებულოა")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "მიღების თარიღი სავალდებულოა")]
        public string Appointment_Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "პირადი ნომერი სავალდებულოა")]
        public int Personal_No { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ექიმის არჩევა სავალდებულოა")]
        public string Doctor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "გთხოვთ მიუთითოთ დრო")]
        public string Appointment_Time { get; set; }
    }
}