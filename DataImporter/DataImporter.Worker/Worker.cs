using Autofac;
using DataImporter.Worker.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly CheckStatusModel _checkStatusModel;
        public Worker(ILogger<Worker> logger, CheckStatusModel checkStatusModel)
        {
            _logger = logger;
            _checkStatusModel = checkStatusModel;
        }
        public static ILifetimeScope AutofacContainer { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _checkStatusModel.CheckCall();
                await Task.Delay(10, stoppingToken);
            }
        }
    }
}
