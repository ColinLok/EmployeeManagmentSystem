using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS_HR_System.Models;
using Microsoft.AspNet.Identity;
using static CS_HR_System.HelperMethods.HelperMethods;

namespace CS_HR_System.Controllers
{
    [Authorize]
    public class PayRollsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PayRolls
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var payRolls = db.PayRolls.Where(p => p.EmployeeId == currentUserId);
            return View(payRolls.ToList());
        }

        // GET: PayRolls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRoll payRoll = db.PayRolls.Find(id);
            if (payRoll == null)
            {
                return HttpNotFound();
            }
            // TODO: check if employee id matches current logged user id when role is employee
            return View(payRoll);
        }

        // GET: PayRolls/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: PayRolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,HourlyRate,HoursWorked,OvertimeHoursWorked,TimeOffHours,TotalPaid,TaxRate,NetPaid,PayrollStartDate,PayrollEndDate")] PayRoll payRoll)
        {
            if (ModelState.IsValid)
            {
                db.PayRolls.Add(payRoll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", payRoll.EmployeeId);
            return View(payRoll);
        }

        // GET: PayRolls/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRoll payRoll = db.PayRolls.Find(id);
            if (payRoll == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", payRoll.EmployeeId);
            return View(payRoll);
        }

        // POST: PayRolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,HourlyRate,HoursWorked,OvertimeHoursWorked,TimeOffHours,TotalPaid,TaxRate,NetPaid,PayrollStartDate,PayrollEndDate")] PayRoll payRoll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payRoll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", payRoll.EmployeeId);
            return View(payRoll);
        }

        // GET: PayRolls/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRoll payRoll = db.PayRolls.Find(id);
            if (payRoll == null)
            {
                return HttpNotFound();
            }
            return View(payRoll);
        }

        // POST: PayRolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayRoll payRoll = db.PayRolls.Find(id);
            db.PayRolls.Remove(payRoll);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: PayRolls/PersonalIndex
        public ActionResult PersonalIndex()
        {
            string currentUserId = User.Identity.GetUserId();
            var payRolls = db.PayRolls.Where(s => s.EmployeeId == currentUserId);
            return View(payRolls.ToList());
        }

        /**
         * Generates a month-based PayRoll for all employees.
         * */
        public ActionResult GeneratePayroll()
        {
            DateTime now = DateTime.Now; // Current date - used mainly for the month
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var employee_list = db.Users.ToList();
            //var shift_list = db.Shifts.ToList();

            // For each employee in the company
            foreach (var employee in employee_list)
            {
                decimal total_hours_worked = 0;
                decimal overtime_hours_worked = 0;
                decimal time_off_hours = 0;

                // Retrieving all shifts for the current employee
                var shift_list = db.Shifts.Where(s => s.EmployeeId == employee.Id
                                                   && s.StartTime.Month == now.Month);

                // For each shift of the selected employee
                foreach (var shift in shift_list)
                {
                    decimal shift_hours_worked = 0;

                    if (shift.RequestedOff)
                    {
                        time_off_hours += 8;    // Shift was requested off
                    }
                    else
                    {
                        shift_hours_worked += shift.PunchOut.Hour - shift.PunchIn.Hour; // Hour difference
                        shift_hours_worked += (shift.PunchOut.Minute - shift.PunchIn.Minute) / 60; // Minute difference

                        if (shift_hours_worked > 8)
                        { // Employee has worked overtime
                            overtime_hours_worked += shift_hours_worked - 8;
                            total_hours_worked += 8;
                        }
                        else
                        {    // Employee worked regular hours
                            total_hours_worked += shift_hours_worked;
                        }
                    }
                }

                // Check how many holidays in the month, apply the holiday hours per(x8)
                //int holidays_in_month = db.Holidays.Find()

                decimal gross_income = (total_hours_worked * employee.HourlyRate)       // regeular hours
                    + (overtime_hours_worked * (employee.HourlyRate * (decimal)1.5))    // overtime hours
                    + (time_off_hours * employee.HourlyRate);                           // time-off hours
                    //+ (holidays_in_month * 8 * employee.HourlyRate)                   // holiday hours
                decimal net_income = gross_income * ((decimal)(1 - 0.12));

                var preExistingPayRoll = db.PayRolls.Any(p => p.EmployeeId == employee.Id &&
                                                                p.PayrollEndDate.Month == now.Month);
                if (preExistingPayRoll)
                {
                    /* Remove an existing PayRoll for the employee is there is one */
                    db.PayRolls.Remove(db.PayRolls.Single(p => p.EmployeeId == employee.Id &&
                                                               p.PayrollEndDate.Month == now.Month));
                }

                /* Create and Insert new Payroll */
                PayRoll newPayRoll = new PayRoll
                {
                    EmployeeId = employee.Id,
                    Employee = employee,
                    HourlyRate = employee.HourlyRate,
                    HoursWorked = total_hours_worked,
                    OvertimeHoursWorked = overtime_hours_worked,
                    TimeOffHours = time_off_hours,
                    TotalPaid = gross_income,
                    TaxRate = (decimal)0.12,
                    NetPaid = net_income,
                    PayrollStartDate = firstDayOfMonth,
                    PayrollEndDate = lastDayOfMonth
                };

                db.PayRolls.Add(newPayRoll);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /**
         * Returns TRUE if the current user is logged in.
         */
        public bool verifyLogIn()
        {
            return (Session["LoginUserID"] != null);
        }

        /**
         * Returns TRUE if the current logged in user is a Manager.
         */
        //public bool authenticateManager()
        //{
        //    string currentUserId = User.Identity.GetUserId();
        //    ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
        //    return (currentUser.JobTitle != 0);
        //}
    }
}
