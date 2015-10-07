using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DryLightning.Data;
using DryLightning.Data.Models;
using DryLightning.Models;
using DryLightning.Repositories;
using Microsoft.AspNet.Identity;

namespace DryLightning.Controllers
{
    public class UserLocationController : Controller
    {

        private IUserLocationRepository _repo;

        public UserLocationController(IUserLocationRepository repo)
        {
            _repo = repo;
        }

        // Controller to display a list of user locations belonging to an user. 
        [Authorize]
        public ActionResult Index()
        {
            UserLocationListDTO ullDto = new UserLocationListDTO();
            string id = User.Identity.GetUserId();
            ullDto.UserLocations = _repo.ListLocations()
                .Where(l => l.UserId == id)
                .Select(l => new UserLocationDetailDTO                  // Projection from the domain model to the UserLocationDetailDTO model.
                {
                    UserLocationId = l.UserLocationId,
                    UserLocationName = l.UserLocationName,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Radius = l.Radius
                })
                .ToList();
            return View(ullDto);
        }

        // Controller to display detailed information of a single user location.
        [Authorize]
        public ActionResult Details(int? id)
        {
            UserLocationDetailDTO uldDto = _repo.ListLocations()
                .Where(l => l.UserLocationId == id)
                .Select(l => new UserLocationDetailDTO                  // Projection from the domain model to the UserLocationDetailDTO model.
                {
                    UserLocationId = l.UserLocationId,
                    UserLocationName = l.UserLocationName,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Radius = l.Radius
                })
                .FirstOrDefault();

            return View(uldDto);
        }

        // Controller to display the create view.
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // Controller to take the user inputs and create an user location entity in the SQL database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(UserLocationCreateDTO ulcDto)
        {
            ulcDto.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                _repo.CreateUserLocation(ulcDto);
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(ulcDto);
        }

        // Controller to take an existing user location entity and displaying to the view.
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLocationEditDTO uleDto = _repo.ListLocations()
                .Where(l => l.UserLocationId == id)
                .Select(l => new UserLocationEditDTO                   // Projection from the domain model to the UserLocationEditDTP model. 
                {
                    UserLocationName = l.UserLocationName,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Radius = l.Radius,
                    UserId = User.Identity.GetUserId(),
                    UserLocationId = l.UserLocationId,
                    LastEmailTime = l.LastEmailTime
                })
                .FirstOrDefault();
            if (uleDto == null)
            {
                return HttpNotFound();
            }
            return View(uleDto);
        }

        // Controller to take the input from the view and update the existing user location entity.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(UserLocationEditDTO uleDto)
        {
            if (ModelState.IsValid)
            {
                _repo.EditUserLocation(uleDto);
                return RedirectToAction("Index");
            }
            return View(uleDto);
        }

        // Controller to display information to be deleted.
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLocationDetailDTO uldDto = _repo.ListLocations()
                .Where(l => l.UserLocationId == id)
                .Select(l => new UserLocationDetailDTO              // Projection from the domain model to the UserLocationDetailDTO model
                {
                    UserLocationName = l.UserLocationName,
                    UserLocationId = l.UserLocationId,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude,
                    Radius = l.Radius
                }).FirstOrDefault();
            
            return View(uldDto);
        }

        // Controller to delete an user location entity.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.DeleteUserLocationConfirmed(id);
            return RedirectToAction("Index");
        }
    }
}
