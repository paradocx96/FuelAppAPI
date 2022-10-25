using System;
using FuelAppAPI.Models;
using FuelAppAPI.DTO;

/*
 * 
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * Converter for FuelStationLog model and DTO
 * 
 * References:
 * https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0
 */

namespace FuelAppAPI.Converters
{
    public class FuelStationLogDtoConverter
    {
        //convert station log DTO without ID to model
        public static FuelStationLogItem convertDtotoModelWithoutId(FuelStationLogDto fuelStationLogDto)
        {
            FuelStationLogItem fuelStationLogItem = new FuelStationLogItem();
            
            fuelStationLogItem.StationId = fuelStationLogDto.StationId;
            fuelStationLogItem.FuelType = fuelStationLogDto.FuelType;
            fuelStationLogItem.FuelStatus = fuelStationLogDto.FuelStatus;
            fuelStationLogItem.DateTime = fuelStationLogDto.DateTime;

            return fuelStationLogItem;
        }

        //convert station log DTO with ID to model
        public static FuelStationLogItem convertDtotoModelWithId(FuelStationLogDto fuelStationLogDto)
        {
            FuelStationLogItem fuelStationLogItem = new FuelStationLogItem();
            fuelStationLogItem.Id = fuelStationLogDto.Id;
            fuelStationLogItem.StationId = fuelStationLogDto.StationId;
            fuelStationLogItem.FuelType = fuelStationLogDto.FuelType;
            fuelStationLogItem.FuelStatus = fuelStationLogDto.FuelStatus;
            fuelStationLogItem.DateTime = fuelStationLogDto.DateTime;

            return fuelStationLogItem;
        }

        //convert station log model without ID to DTO
        public static FuelStationLogDto convertModelToDtoWithoutId(FuelStationLogItem fuelStationLogItem)
        {
            FuelStationLogDto fuelStationLogDto = new FuelStationLogDto();

            fuelStationLogDto.StationId = fuelStationLogItem.StationId;
            fuelStationLogDto.FuelType = fuelStationLogItem.FuelType;
            fuelStationLogDto.FuelStatus = fuelStationLogItem.FuelStatus;
            fuelStationLogDto.DateTime = fuelStationLogItem.DateTime;

            //get the date time
            DateTime dateTime = fuelStationLogItem.DateTime ?? DateTime.Now;

            fuelStationLogDto.Year = dateTime.Year;
            fuelStationLogDto.Month = dateTime.Month;
            fuelStationLogDto.DayNumber = dateTime.Day;
            fuelStationLogDto.Hour = dateTime.Hour;
            fuelStationLogDto.Minute = dateTime.Minute;
            fuelStationLogDto.Second = dateTime.Second;

            return fuelStationLogDto;
        }

        //convert station log model with ID to DTO
        public static FuelStationLogDto convertModelToDtoWithId(FuelStationLogItem fuelStationLogItem)
        {
            FuelStationLogDto fuelStationLogDto = new FuelStationLogDto();
            fuelStationLogDto.Id = fuelStationLogItem.Id;
            fuelStationLogDto.StationId = fuelStationLogItem.StationId;
            fuelStationLogDto.FuelType = fuelStationLogItem.FuelType;
            fuelStationLogDto.FuelStatus = fuelStationLogItem.FuelStatus;
            fuelStationLogDto.DateTime = fuelStationLogItem.DateTime;

            //get the date time
            DateTime dateTime = fuelStationLogItem.DateTime ?? DateTime.Now;

            fuelStationLogDto.Year = dateTime.Year;
            fuelStationLogDto.Month = dateTime.Month;
            fuelStationLogDto.DayNumber = dateTime.Day;
            fuelStationLogDto.Hour = dateTime.Hour;
            fuelStationLogDto.Minute = dateTime.Minute;
            fuelStationLogDto.Second = dateTime.Second;

            return fuelStationLogDto;
        }
    }
}

