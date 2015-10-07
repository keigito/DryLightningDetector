using DryLightning.Data;
using DryLightning.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using DryLightning.Models;

namespace DryLightning.Repositories
{
    public class UserLocationRepository : IUserLocationRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // A method to create a list of User Locations
        public IEnumerable<UserLocation> ListLocations()
        {
            var userLocations = _db.UserLocations.Include(u => u.User);
            return userLocations;
        }

        // A method to create the User Location
        public void CreateUserLocation(UserLocationCreateDTO ulcDto)
        {
            // ulcDto to userLocation model mapping
            UserLocation userLocation = new UserLocation();

            userLocation.UserLocationName = ulcDto.UserLocationName;
            userLocation.Latitude = ulcDto.Latitude;
            userLocation.Longitude = ulcDto.Longitude;
            userLocation.LastEmailTime = DateTime.Now.AddHours(-1.0);
            userLocation.Radius = ulcDto.Radius;
            userLocation.UserId = ulcDto.UserId;

            _db.UserLocations.Add(userLocation);
            _db.SaveChanges();
        }

        // A method to edit the User Location
        public void EditUserLocation(UserLocationEditDTO uleDto)
        {
            // uleDto to userLocation model mapping
            UserLocation userLocation = new UserLocation();

            userLocation.UserLocationName = uleDto.UserLocationName;
            userLocation.Latitude = uleDto.Latitude;
            userLocation.Longitude = uleDto.Longitude;
            userLocation.Radius = uleDto.Radius;
            userLocation.UserId = uleDto.UserId;
            userLocation.LastEmailTime = uleDto.LastEmailTime;
            userLocation.UserLocationId = uleDto.UserLocationId;

            _db.Entry(userLocation).State = EntityState.Modified;
            _db.SaveChanges();
        }

        // A method to delete the User Location
        public void DeleteUserLocationConfirmed(int id)
        {
            var userLocation = this.ListLocations().FirstOrDefault(l => l.UserLocationId == id);
            _db.UserLocations.Remove(userLocation);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}