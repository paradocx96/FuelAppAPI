using System;
using FuelAppAPI.Models;
using MongoDB.Driver;

namespace FuelAppAPI.Services
{
    public class QueueLogService
    {
        private readonly IMongoCollection<QueueLogItem> _queueLogItemCollection;
        public QueueLogService()
        {
        }
    }
}

