using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS_HR_System.Models;
using Microsoft.AspNet.Identity;

namespace CS_HR_System.Controllers
{
    [Authorize]
    public class ShiftsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shifts
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            var shifts = db.Shifts.Include(s => s.Employee);
            return View(shifts.ToList());
        }

        // GET: Shifts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            // TODO: check if shift employee id matches current logged user id when role is employee
            return View(shift);
        }

        // GET: Shifts/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Users , "Id", "Email");

            Debug.WriteLine(db.Users.ToList()[0].Id);

            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,RequestedOff,StartTime,EndTime,Date,PunchIn,PunchOut")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Shifts.Add(shift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", shift.EmployeeId);
            return View(shift);
        }

        // GET: Shifts/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", shift.EmployeeId);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,RequestedOff,StartTime,EndTime,Date,PunchIn,PunchOut")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", shift.EmployeeId);
            return View(shift);
        }

        // GET: Shifts/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shift shift = db.Shifts.Find(id);
            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shift shift = db.Shifts.Find(id);
            db.Shifts.Remove(shift);
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

        // GET: Shifts/PersonalIndex
        public ActionResult PersonalIndex()
        {
            string currentUserId = User.Identity.GetUserId();
            var shifts = db.Shifts.Where(s => s.EmployeeId == currentUserId);
            return View(shifts.ToList());
        }
    }
}
