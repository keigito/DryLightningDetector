using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DryLightning.Models
{
    // A list of UserLocationDetailDTO
    public class UserLocationListDTO
    {
        public IList<UserLocationDetailDTO> UserLocations { get; set; }
    }

    // DTO for the details view
    public class UserLocationDetailDTO
    {
        public int UserLocationId { get; set; }
        [DisplayName("Location Name")]
        public string UserLocationName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Radius { get; set; }
    }


    // DTO for creating new userLocation entity
    public class UserLocationCreateDTO
    {
        [DisplayName("Location Name")]
        public string UserLocationName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Radius { get; set; }
        public string UserId { get; set; }
    }

    // DTO for editing userLocation entity
    public class UserLocationEditDTO
    {
        public int UserLocationId { get; set; }
        [DisplayName("Location Name")]
        public string UserLocationName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Radius { get; set; }
        public string UserId { get; set; }
        public DateTime LastEmailTime { get; set; }
    }
}