using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ClinicSakurso.Models;

namespace ClinicSakurso.Controllers
{
    public class HomeController : Controller
    {
        public static List<string> AppTime = new List<string>();
        public static List<string> DoctorNames = new List<string>();
        public static List<string> DepNames = new List<string>();
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Doctor> DoctorDetails = new List<Doctor>();
        public static List<Patient> PatientDetails = new List<Patient>();
        public static List<Patient> PatientData = new List<Patient>();
        public static List<Department> DepartmentData = new List<Department>();
        public static List<Room> RoomData = new List<Room>();

        public static int userId;

        ClinicEntities CE = new ClinicEntities();

        public ActionResult Index() { Doctors.Clear(); Doctors = CE.Doctors.ToList(); return View(); }

        [HttpGet]
        public ActionResult Patients() { PatientDetails = CE.Patients.ToList(); return View(); }

        [HttpGet]
        public ActionResult AddDoctor() { return View(); }

        [HttpGet]
        public ActionResult Department() { DepartmentData = CE.Departments.ToList(); return View(); }

        [HttpGet]
        public ActionResult AddDepartment() { return View(); }

        [HttpGet]
        public ActionResult Room() { RoomData = CE.Rooms.ToList(); return View(); }

        [HttpGet]
        public ActionResult AddPatient()
        {
            DoctorNames.Clear();
            AppTime.Clear();

            AppTime.Add("11:00");
            AppTime.Add("11:30");
            AppTime.Add("12:00");
            AppTime.Add("12:30");
            AppTime.Add("13:00");
            AppTime.Add("13:30");
            AppTime.Add("14:00");
            AppTime.Add("15:30");
            AppTime.Add("16:00");
            AppTime.Add("16:30");
            AppTime.Add("17:00");
            AppTime.Add("18:00");
            AppTime.Add("18:30");

            List<Doctor> Doctors = new List<Doctor>();

            Doctors = CE.Doctors.ToList();
            for (int i = 0; i < Doctors.Count; i++) { DoctorNames.Add(Doctors[i].First_Name + " " + Doctors[i].Last_Name); }

            return View();
        }

        [HttpPost]
        public ActionResult AddDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                CE.Doctors.Add(doctor);
                CE.SaveChanges();

                ViewBag.Msg = "ექიმი წარმატებით დაემატა";
                return View();
            }
            else { return View(); }
        }

        [HttpGet]
        public ActionResult EditDoctor(int UserId)
        {
            if (UserId == 0) { return RedirectToAction("Index", "Home"); }
            else
            {
                DoctorDetails.Clear();
                DoctorDetails = CE.Doctors.Where(m => m.Id == UserId).ToList();
                userId = UserId;

                return View();
            }
        }

        [HttpPost]
        public ActionResult EditDoctor(DoctorModel doctor)
        {
            if (ModelState.IsValid)
            {
                var user = CE.Doctors.Where(a => a.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.First_Name = doctor.First_Name;
                    user.Last_Name = doctor.Last_Name;
                    user.Department = doctor.Department;
                    user.Phone = doctor.Phone;
                    user.Room = doctor.Room;

                    CE.SaveChanges();
                    ViewBag.Msg = "მონაცემები წარმატებით განახლდა";
                    EditDoctor(userId);
                }
                return EditDoctor(userId);
            }
            else { ViewBag.Msg = "Err"; return View(); }
        }

        [HttpGet]
        public ActionResult EditPatient(int patientId)
        {
            if (patientId == 0) { return RedirectToAction("Index", "Home"); }
            else
            {
                PatientData.Clear();
                PatientData = CE.Patients.Where(m => m.Id == patientId).ToList();
                userId = patientId;

                return View();
            }
        }

        [HttpPost]
        public ActionResult EditPatient(PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                var user = CE.Patients.Where(a => a.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.First_Name = patient.First_Name;
                    user.Last_Name = patient.Last_Name;
                    user.Phone = patient.Phone;
                    user.Doctor = patient.Doctor;
                    user.Personal_No = patient.Personal_No;
                    user.Appointment_Date = patient.Appointment_Date;
                    user.Appointment_Time = patient.Appointment_Time;

                    CE.SaveChanges();
                    ViewBag.Msg = "მონაცემები წარმატებით განახლდა";
                    EditPatient(userId);
                }
                return EditPatient(userId);
            }
            else { ViewBag.Msg = "Err"; return View(); }
        }

        [HttpGet]
        public ActionResult EditDepartment(int DepId)
        {
            if (DepId == 0) { return RedirectToAction("Index", "Home"); }
            else
            {
                DepartmentData.Clear();
                DepartmentData = CE.Departments.Where(m => m.Id == DepId).ToList();
                userId = DepId;
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditDepartment(DepartmentMetaData dep)
        {
            if (ModelState.IsValid)
            {
                var user = CE.Departments.Where(a => a.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.Name = dep.Name;
                    user.Workers = dep.Workers;
                    user.Rooms = dep.Rooms;

                    CE.SaveChanges();
                    ViewBag.Msg = "მონაცემები წარმატებით განახლდა";
                    EditDepartment(userId);
                }
                return EditDepartment(userId);
            }
            else { ViewBag.Msg = "Err"; return View(); }
        }

        [HttpPost]
        public ActionResult AddPatient(Patient patient, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string ChoosenDoctor = Request.Form["Doctor"].ToString();
                string ChoosenTime = Request.Form["Appointment_Time"].ToString();

                if (ChoosenDoctor == "") { ModelState.AddModelError("ChooseDoctor", "გთხოვთ არჩიოთ ექიმი"); return View(); }
                if (ChoosenTime == "") { ModelState.AddModelError("ChooseTime", "გთხოვთ არჩიოთ დრო"); return View(); }

                if (ChoosenDoctor != "" && ChoosenTime != "")
                {
                    patient.Doctor = ChoosenDoctor;
                    patient.Appointment_Time = ChoosenTime;
                    patient.Appointment_Date = Convert.ToDateTime(patient.Appointment_Date).ToShortDateString();

                    using (ClinicEntities ce = new ClinicEntities()) { ce.Patients.Add(patient); ce.SaveChanges(); ViewBag.Msg = "ახალი პაციენტი წარმატებით დაემატა"; }
                }
                else { return View(); }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Department dep) { if (ModelState.IsValid) { CE.Departments.Add(dep); CE.SaveChanges(); return RedirectToAction("Department", "Home"); } else { return View(); } }

        [HttpGet]
        public ActionResult AddRoom()
        {
            DepNames.Clear();
            List<Department> deps = new List<Department>();
            deps = CE.Departments.ToList();
            for (int i = 0; i < deps.Count; i++) { DepNames.Add(deps[i].Name); }
            return View();
        }

        [HttpPost]
        public ActionResult AddRoom(Room room, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string ChoosenDep = Request.Form["Department"].ToString();

                if (ChoosenDep == "აირჩიეთ") { ModelState.AddModelError("ChooseDep", "გთხოვთ არჩიოთ დეპარტამენტი"); return View(); }

                if (ChoosenDep != "აირჩიეთ") { room.Department = ChoosenDep; CE.Rooms.Add(room); CE.SaveChanges(); return RedirectToAction("Room", "Home"); }
            }
            else { return View(); }
            return View();
        }

        [HttpGet]
        public ActionResult EditRoom(int RoomId)
        {
            if (RoomId == 0) { return RedirectToAction("Index", "Home"); }
            else
            {
                RoomData.Clear();
                RoomData = CE.Rooms.Where(m => m.Id == RoomId).ToList();
                userId = RoomId;
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditRoom(RoomMetaData room)
        {
            if (ModelState.IsValid)
            {
                var user = CE.Rooms.Where(a => a.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.Block = room.Block;
                    user.Department = room.Department;
                    user.Floor = room.Floor;

                    CE.SaveChanges();
                    ViewBag.Msg = "მონაცემები წარმატებით განახლდა";
                    EditRoom(userId);
                }
                return EditRoom(userId);
            }
            else { ViewBag.Msg = "Err"; return View(); }
        }

        public ActionResult DeleteDoctor(FormCollection form)
        {
            string RecordId = Request.Form["DeleteDoctor"].ToString();
            int Id = Convert.ToInt32(RecordId);
            var Doc = CE.Doctors.Find(Id);
            CE.Doctors.Remove(Doc);
            CE.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletePatient(FormCollection form)
        {
            string RecordId = Request.Form["DeletePatient"].ToString();
            int Id = Convert.ToInt32(RecordId);
            var Patient = CE.Patients.Find(Id);
            CE.Patients.Remove(Patient);
            CE.SaveChanges();
            return RedirectToAction("Patients", "Home");
        }

        public ActionResult DeleteDepartment(FormCollection form)
        {
            string RecordId = Request.Form["DeleteDep"].ToString();
            int Id = Convert.ToInt32(RecordId);
            var DepartmentData = CE.Departments.Find(Id);
            CE.Departments.Remove(DepartmentData);
            CE.SaveChanges();
            return RedirectToAction("Department", "Home");
        }

        public ActionResult DeleteRoom(FormCollection form)
        {
            string RecordId = Request.Form["DeleteRoom"].ToString();
            int Id = Convert.ToInt32(RecordId);
            var RoomsData = CE.Rooms.Find(Id);
            CE.Rooms.Remove(RoomsData);
            CE.SaveChanges();
            return RedirectToAction("Department", "Home");
        }
    }
}