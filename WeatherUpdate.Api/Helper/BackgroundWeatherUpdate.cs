using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherUpdate.Service.Interface;

namespace WeatherUpdate.Api.Helper
{
    public class BackgroundWeatherUpdate : IHostedService
    {
        private Timer Timer;

        private readonly IServiceProvider serviceProvider;

        public BackgroundWeatherUpdate(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }


        //Background service that will run every 10 secs. this will call update method in each of 10 secs after application runs.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Timer = new Timer(x => CallUpdateRepo(),
              null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task CallUpdateRepo()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IWeatherRepository>();
                await scopedService.UpdateWeatherDetailsAsync();

            }
        }
    }
}
