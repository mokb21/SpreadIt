using System;
using System.Collections.Generic;
using System.Text;
using SpreadIt.Repository.Models;

namespace SpreadIt.Repository.Factories
{
    public class LocationFactory
    {
        public LocationFactory()
        {

        }

        public DTO.Location CreateLocation(Location location)
        {
            return new DTO.Location()
            {
                Id = location.Id,
                Name = location.Name,
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }



        public Location CreateLocation(DTO.Location location)
        {
            return new Location()
            {
                Id = location.Id,
                Name = location.Name,
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }
    }
}
