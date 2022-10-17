using System;
using FuelAppAPI.DTO;
using FuelAppAPI.Models;


/*
 * IT19014128
 * A.M.W.W.R.L. Watketiya
 * 
 * This class does converssions between fuel station DTOs and fuel station models
 */

namespace FuelAppAPI.Converters
{
    public class FuelStationDtoConverter
    {
        //convert fuel station DTO with no ID to Fuel Station Model
        public static FuelStation convertDtoToModelWithoutId(FuelStationDto fuelStationDto)
        {
            FuelStation fuelStation = new FuelStation();
            // populate fuel station data
            fuelStation.License = fuelStationDto.License;
            fuelStation.OwnerUsername = fuelStationDto.OwnerUsername;
            fuelStation.StationName = fuelStationDto.StationName;
            fuelStation.StationAddress = fuelStationDto.StationAddress;
            fuelStation.StationPhoneNumber = fuelStationDto.StationPhoneNumber;
            fuelStation.StationEmail = fuelStationDto.StationEmail;
            fuelStation.StationWebsite = fuelStationDto.StationWebsite;

            // populate fuel station status data with initial data
            // initial status data may be dummy data
            fuelStation.OpenStatus = fuelStationDto.OpenStatus;
            fuelStation.PetrolQueueLength = fuelStationDto.PetrolQueueLength;
            fuelStation.DieselQueueLength = fuelStationDto.DieselQueueLength;
            fuelStation.PetrolStatus = fuelStationDto.PetrolStatus;
            fuelStation.DieselStatus = fuelStationDto.DieselStatus;

            //populate the fuel station location details
            // location implementation might no be complete
            // location data is stored in the database
            fuelStation.LocationLatitude = fuelStationDto.LocationLatitude;
            fuelStation.LocationLongitude = fuelStationDto.LocationLongitude;

            //return the fuel station
            return fuelStation;
        }

        //convert fuel station DTO with ID to Fuel Station Model
        public static FuelStation convertDtoToModelWithId(FuelStationDto fuelStationDto)
        {
            FuelStation fuelStation = new FuelStation();
            // populate fuel station data
            fuelStation.Id = fuelStationDto.Id;
            fuelStation.License = fuelStationDto.License;
            fuelStation.OwnerUsername = fuelStationDto.OwnerUsername;
            fuelStation.StationName = fuelStationDto.StationName;
            fuelStation.StationAddress = fuelStationDto.StationAddress;
            fuelStation.StationPhoneNumber = fuelStationDto.StationPhoneNumber;
            fuelStation.StationEmail = fuelStationDto.StationEmail;
            fuelStation.StationWebsite = fuelStationDto.StationWebsite;

            // populate fuel station status data with initial data
            // initial status data may be dummy data
            fuelStation.OpenStatus = fuelStationDto.OpenStatus;
            fuelStation.PetrolQueueLength = fuelStationDto.PetrolQueueLength;
            fuelStation.DieselQueueLength = fuelStationDto.DieselQueueLength;
            fuelStation.PetrolStatus = fuelStationDto.PetrolStatus;
            fuelStation.DieselStatus = fuelStationDto.DieselStatus;

            //populate the fuel station location details
            // location implementation might no be complete
            // location data is stored in the database
            fuelStation.LocationLatitude = fuelStationDto.LocationLatitude;
            fuelStation.LocationLongitude = fuelStationDto.LocationLongitude;

            //return the fuel station
            return fuelStation;
        }

        //convert FuelStation Model with no Id to Fuel Station DTO
        public static FuelStationDto convertModelToDtoWithNoId(FuelStation fuelStation)
        {
            FuelStationDto fuelStationDto = new FuelStationDto();

            // populate fuel station data
            fuelStationDto.License = fuelStation.License;
            fuelStationDto.OwnerUsername = fuelStation.OwnerUsername;
            fuelStationDto.StationName = fuelStation.StationName;
            fuelStationDto.StationAddress = fuelStation.StationAddress;
            fuelStationDto.StationPhoneNumber = fuelStation.StationPhoneNumber;
            fuelStationDto.StationEmail = fuelStation.StationEmail;
            fuelStationDto.StationWebsite = fuelStation.StationWebsite;

            // populate fuel station status data with initial data
            // initial status data may be dummy data
            fuelStationDto.OpenStatus = fuelStation.OpenStatus;
            fuelStationDto.PetrolQueueLength = fuelStation.PetrolQueueLength;
            fuelStationDto.DieselQueueLength = fuelStation.DieselQueueLength;
            fuelStationDto.PetrolStatus = fuelStation.PetrolStatus;
            fuelStationDto.DieselStatus = fuelStation.DieselStatus;

            //populate the fuel station location details
            // location implementation might no be complete
            // location data is stored in the database
            fuelStationDto.LocationLatitude = fuelStation.LocationLatitude;
            fuelStationDto.LocationLongitude = fuelStation.LocationLongitude;

            //return the DTO
            return fuelStationDto;

        }

        //convert FuelStation Model with Id to Fuel Station DTO
        public static FuelStationDto convertModelToDtoWithId(FuelStation fuelStation)
        {
            FuelStationDto fuelStationDto = new FuelStationDto();

            // populate fuel station data
            fuelStationDto.Id = fuelStationDto.Id;
            fuelStationDto.License = fuelStation.License;
            fuelStationDto.OwnerUsername = fuelStation.OwnerUsername;
            fuelStationDto.StationName = fuelStation.StationName;
            fuelStationDto.StationAddress = fuelStation.StationAddress;
            fuelStationDto.StationPhoneNumber = fuelStation.StationPhoneNumber;
            fuelStationDto.StationEmail = fuelStation.StationEmail;
            fuelStationDto.StationWebsite = fuelStation.StationWebsite;

            // populate fuel station status data with initial data
            // initial status data may be dummy data
            fuelStationDto.OpenStatus = fuelStation.OpenStatus;
            fuelStationDto.PetrolQueueLength = fuelStation.PetrolQueueLength;
            fuelStationDto.DieselQueueLength = fuelStation.DieselQueueLength;
            fuelStationDto.PetrolStatus = fuelStation.PetrolStatus;
            fuelStationDto.DieselStatus = fuelStation.DieselStatus;

            //populate the fuel station location details
            // location implementation might no be complete
            // location data is stored in the database
            fuelStationDto.LocationLatitude = fuelStation.LocationLatitude;
            fuelStationDto.LocationLongitude = fuelStation.LocationLongitude;

            //return the DTO
            return fuelStationDto;

        }
    }
}

