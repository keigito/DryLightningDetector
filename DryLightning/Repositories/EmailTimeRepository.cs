using DryLightning.Data;
using DryLightning.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DryLightning.Repositories
{
    public class EmailTimeRepository : IEmailTimeRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public void UpdateLastEmailTime(int id)
        {
            UserLocation userLocation = _db.UserLocations.FirstOrDefault(l => l.UserLocationId == id);
            userLocation.LastEmailTime = DateTime.Now;
            _db.Entry(userLocation).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}