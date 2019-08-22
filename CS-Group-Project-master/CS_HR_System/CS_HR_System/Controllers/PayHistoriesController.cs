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

namespace CS_HR_System.Controllers
{
    [Authorize]
    public class PayHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PayHistories
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            var payHistories = db.PayHistories.Include(p => p.Employee);
            return View(payHistories.ToList());
        }

        // GET: PayHistories/Details/5
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayHistory payHistory = db.PayHistories.Find(id);
            if (payHistory == null)
            {
                return HttpNotFound();
            }
            return View(payHistory);
        }

        // GET: PayHistories/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: PayHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,SalaryBefore,SalaryAfter,HourlyBefore,HourlyAfter,DateApplied")] PayHistory payHistory)
        {
            if (ModelState.IsValid)
            {
                db.PayHistories.Add(payHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", payHistory.EmployeeId);
            return View(payHistory);
        }

        // GET: PayHistories/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayHistory payHistory = db.PayHistories.Find(id);
            if (payHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", payHistory.EmployeeId);
            return View(payHistory);
        }

        // POST: PayHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,SalaryBefore,SalaryAfter,HourlyBefore,HourlyAfter,DateApplied")] PayHistory payHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Email", payHistory.EmployeeId);
            return View(payHistory);
        }

        // GET: PayHistories/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayHistory payHistory = db.PayHistories.Find(id);
            if (payHistory == null)
            {
                return HttpNotFound();
            }
            return View(payHistory);
        }

        // POST: PayHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PayHistory payHistory = db.PayHistories.Find(id);
            db.PayHistories.Remove(payHistory);
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

        // GET: PayHistories/PersonalIndex
        public ActionResult PersonalIndex()
        {
            string currentUserId = User.Identity.GetUserId();
            var payHistories = db.PayHistories.Where(s => s.EmployeeId == currentUserId);
            return View(payHistories.ToList());
        }
    }
}
