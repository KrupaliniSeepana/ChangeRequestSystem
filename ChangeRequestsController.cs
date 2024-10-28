using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChangeRequestSystem.Models;

namespace ChangeRequestSystem.Controllers
{
    public class ChangeRequestsController : Controller
    {
        private YourDbContext db = new YourDbContext();

        // GET: ChangeRequests
        public ActionResult Index()
        {
            return View(db.ChangeRequests.ToList());
        }

        // GET: ChangeRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeRequest changeRequest = db.ChangeRequests.Find(id);
            if (changeRequest == null)
            {
                return HttpNotFound();
            }
            return View(changeRequest);
        }

        // GET: ChangeRequests/Create
        [Authorize(Roles = "Requester")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChangeRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Requester")]
        public ActionResult Create([Bind(Include = "RequestID,Title,Description,Priority,DueDate,Status,CreatedDate,ProjectID,RequestedBy,ApprovedBy,AssignedTeamID")] ChangeRequest changeRequest)
        {
            if (ModelState.IsValid)
            {
                changeRequest.Status = "Pending";
                changeRequest.CreatedDate = DateTime.Now;
                db.ChangeRequests.Add(changeRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(changeRequest);
        }

        // GET: Approve a Change Request (Manager Only)
        [Authorize(Roles = "Manager")]
        public ActionResult Approve(int? requestId)
        {
            if (requestId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var changeRequest = db.ChangeRequests.Find(requestId);
            if (changeRequest == null || changeRequest.Status != "Pending")
            {
                return HttpNotFound();
            }
            return View(changeRequest);
        }

        // POST: Approve or Reject a Change Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Approve(int requestId, bool approve, string comments)
        {
            var request = db.ChangeRequests.Find(requestId);
            if (request != null && request.Status == "Pending")
            {
                request.Status = approve ? "Approved" : "Rejected";
                request.Comments = comments;
                request.ApprovedBy = GetCurrentUserId(); // Add logic to get the current user ID
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // GET: Assign a Development Team to Change Request (Manager Only)
        [Authorize(Roles = "Manager")]
        public ActionResult AssignTeam(int? requestId)
        {
            if (requestId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var request = db.ChangeRequests.Find(requestId);
            if (request == null || request.Status != "Approved")
            {
                return HttpNotFound();
            }

            // Pass a list of teams to the view
            ViewBag.Teams = new SelectList(db.Teams, "TeamID", "TeamName");
            return View(request);
        }

        // POST: Assign a Development Team to Change Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult AssignTeam(int requestId, int teamId)
        {
            var request = db.ChangeRequests.Find(requestId);
            if (request != null && request.Status == "Approved")
            {
                request.AssignedTeamID = teamId;
                request.Status = "In Development";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        // GET: ChangeRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeRequest changeRequest = db.ChangeRequests.Find(id);
            if (changeRequest == null)
            {
                return HttpNotFound();
            }
            return View(changeRequest);
        }

        // POST: ChangeRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,Title,Description,Priority,DueDate,Status,CreatedDate,ProjectID,RequestedBy,ApprovedBy,AssignedTeamID")] ChangeRequest changeRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(changeRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(changeRequest);
        }

        // GET: ChangeRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeRequest changeRequest = db.ChangeRequests.Find(id);
            if (changeRequest == null)
            {
                return HttpNotFound();
            }
            return View(changeRequest);
        }

        // POST: ChangeRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChangeRequest changeRequest = db.ChangeRequests.Find(id);
            db.ChangeRequests.Remove(changeRequest);
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

        // Helper Method to Get the Current User ID
        private int GetCurrentUserId()
        {
            // Placeholder for actual user ID retrieval logic
            // Implement logic to retrieve the currently logged-in user's ID
            return 1; // Replace with actual logic
        }
    }
}
