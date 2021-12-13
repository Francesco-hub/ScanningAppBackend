using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.ApplicationService.Services;
using ScanningApp.Core.DomainService;
using ScanningApp.Core.Entity;
using ScanningApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ScanningAppContext _concertService;
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private List<Concert> _websiteConcerts;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)

        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _concertService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ScanningAppContext>();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }


        public IEnumerable<String> EEConcerts { get; set; }

        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var httpClient = _httpClientFactory.CreateClient();
                _websiteConcerts = new List<Concert>();
                List<Concert> databaseConcerts = new List<Concert>();
                var counter = 1;
                // string replacement = String.Replace(" &#8211; ", "-");
                while (true)
                    try
                    {
                        databaseConcerts = getCurrentConcertList();
                        var responseStream = await httpClient.GetStreamAsync("https://esbjergensemble.com/wp-json/tribe/events/v1/events/?page=" + counter);
                        var concertList = await JsonSerializer.DeserializeAsync<EventDTO>(responseStream, _options);
                        foreach (var concert in concertList.Events)
                        {

                            if (concert.title.Contains(" &#8211; "))
                            {
                                concert.title = concert.title.Replace(" &#8211; ", "-");
                            }
                            if (concert.title.Contains("&#8217;"))
                            {
                                concert.title = concert.title.Replace("&#8217;", "'");
                            }
                            _websiteConcerts.Add(concert);

                        }
                        counter++;
                    }

                    catch (Exception e)
                    {

                        updateConcertList(databaseConcerts, _websiteConcerts);
                    }

                await Task.Delay(1000, stoppingToken);
            }
        }

        public List<Concert> getCurrentConcertList()
        {
            return _concertService.Concerts.Where(c => DateTime.Compare(c.start_date, DateTime.Now) >= 0).ToList(); ;
        }


        public void updateConcertList(List<Concert> websiteConcerts, List<Concert> databaseConcerts)
        {
            if (websiteConcerts == databaseConcerts)
            {
                _logger.LogInformation("updateConcertList website = database");

                foreach (var item in websiteConcerts)
                {
                    //Add new concerts

                }


                return;
            }

        }
    }
}
