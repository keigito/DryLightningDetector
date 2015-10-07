using System.Collections.Generic;
using DryLightning.Data.Models;
using DryLightning.Models;

namespace DryLightning.Repositories
{
    // 
    public interface IUserLocationRepository
    {
        void CreateUserLocation(UserLocationCreateDTO ulcDto);
        void DeleteUserLocationConfirmed(int id);
        void EditUserLocation(UserLocationEditDTO uleDto);
        IEnumerable<UserLocation> ListLocations();
    }
}