using System;
using FuelAppAPI.Models;
using FuelAppAPI.DTO;

/*
 * 
 * IT19014128
 * A.M.W.W.R.L. Wataketiya
 * Converter for QueueLogItem model and DTO
 */

namespace FuelAppAPI.Converters
{
    public class QueueLogDtoConverter
    {
        //convert queue log DTO without ID to queue log model
        public static QueueLogItem convertDtoToModelWithoutId(QueueLogItemDto queueLogItemDto)
        {
            QueueLogItem queueLogItem = new QueueLogItem();

            //populate queue log item data
            queueLogItem.CustomerUsername = queueLogItemDto.CustomerUsername;
            queueLogItem.StationId = queueLogItemDto.StationId;
            queueLogItem.StationLicense = queueLogItemDto.StationLicense;
            queueLogItem.StationName = queueLogItemDto.StationName;
            queueLogItem.Queue = queueLogItemDto.Queue;
            queueLogItem.Action = queueLogItemDto.Action;
            queueLogItem.RefuelStatus = queueLogItemDto.RefuelStatus;
            queueLogItem.dateTime = queueLogItemDto.dateTime;

            return queueLogItem;
        }

        //convert queue log DTO with ID to queue log model
        public static QueueLogItem convertDtoToModelWithId(QueueLogItemDto queueLogItemDto)
        {
            QueueLogItem queueLogItem = new QueueLogItem();

            //populate queue log item data
            queueLogItem.Id = queueLogItemDto.Id;
            queueLogItem.CustomerUsername = queueLogItemDto.CustomerUsername;
            queueLogItem.StationId = queueLogItemDto.StationId;
            queueLogItem.StationLicense = queueLogItemDto.StationLicense;
            queueLogItem.StationName = queueLogItemDto.StationName;
            queueLogItem.Queue = queueLogItemDto.Queue;
            queueLogItem.Action = queueLogItemDto.Action;
            queueLogItem.RefuelStatus = queueLogItemDto.RefuelStatus;
            queueLogItem.dateTime = queueLogItemDto.dateTime;

            return queueLogItem;
        }

        //convert queue log model without id to queue log DTO
        public static QueueLogItemDto convertModelToDtoWithoutId(QueueLogItem queueLogItem)
        {
            QueueLogItemDto queueLogItemDto = new QueueLogItemDto();

            //populate DTO data
            queueLogItemDto.CustomerUsername = queueLogItem.CustomerUsername;
            queueLogItemDto.StationId = queueLogItem.StationId;
            queueLogItemDto.StationLicense = queueLogItem.StationLicense;
            queueLogItemDto.StationName = queueLogItem.StationName;
            queueLogItemDto.Queue = queueLogItem.Queue;
            queueLogItemDto.Action = queueLogItem.Action;
            queueLogItemDto.RefuelStatus = queueLogItem.RefuelStatus;

            //get the date time
            DateTime dateTime = queueLogItem.dateTime ?? DateTime.Now;
            //set the date and time details seperately
            queueLogItemDto.Year = dateTime.Year;
            queueLogItemDto.Month = dateTime.Month;
            queueLogItemDto.DayNumber = dateTime.Day;
            queueLogItemDto.Hour = dateTime.Hour;
            queueLogItemDto.Minute = dateTime.Minute;
            queueLogItemDto.Second = dateTime.Second;
            queueLogItemDto.dateTime = queueLogItem.dateTime;

            return queueLogItemDto;
        }

        //convert queue log model with id to queue log DTO
        public static QueueLogItemDto convertModelToDtoWithId(QueueLogItem queueLogItem)
        {
            QueueLogItemDto queueLogItemDto = new QueueLogItemDto();

            //populate DTO data
            queueLogItemDto.Id = queueLogItem.Id;
            queueLogItemDto.CustomerUsername = queueLogItem.CustomerUsername;
            queueLogItemDto.StationId = queueLogItem.StationId;
            queueLogItemDto.StationLicense = queueLogItem.StationLicense;
            queueLogItemDto.StationName = queueLogItem.StationName;
            queueLogItemDto.Queue = queueLogItem.Queue;
            queueLogItemDto.Action = queueLogItem.Action;
            queueLogItemDto.RefuelStatus = queueLogItem.RefuelStatus;


            //get the date time
            DateTime dateTime = queueLogItem.dateTime ?? DateTime.Now;
            //set the date and time details seperately
            queueLogItemDto.Year = dateTime.Year;
            queueLogItemDto.Month = dateTime.Month;
            queueLogItemDto.DayNumber = dateTime.Day;
            queueLogItemDto.Hour = dateTime.Hour;
            queueLogItemDto.Minute = dateTime.Minute;
            queueLogItemDto.Second = dateTime.Second;
            queueLogItemDto.dateTime = queueLogItem.dateTime;

            return queueLogItemDto;
        }
    }
}

