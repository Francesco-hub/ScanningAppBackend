using Microsoft.EntityFrameworkCore;
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
        private List<Concert> _databaseConcerts;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)

        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _concertService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ScanningAppContext>();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }    

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var httpClient = _httpClientFactory.CreateClient();
                _websiteConcerts = new List<Concert>();
                _databaseConcerts = new List<Concert>();
                var counter = 1;
                while (true)
                    try
                    {
                        _databaseConcerts = getCurrentConcertList();
                        var responseStream = await httpClient.GetStreamAsync("https://esbjergensemble.com/wp-json/tribe/events/v1/events/?page=" + counter);
                        var concertList = await JsonSerializer.DeserializeAsync<EventDTO>(responseStream);
                        foreach (var concert in concertList.events)
                        {

                            if (concert.title.Contains(" &#8211; "))
                            {
                                concert.title = concert.title.Replace(" &#8211; ", "-");
                            }
                            if (concert.title.Contains("&#8217;"))
                            {
                                concert.title = concert.title.Replace("&#8217;", "'");
                            }

                            var temp_concert = new Concert();
                            temp_concert.id = concert.id;
                            temp_concert.title = concert.title;
                            temp_concert.start_date = DateTime.Parse(concert.start_date);    
                            _websiteConcerts.Add(temp_concert);

                        }
                        counter++;
                    }

                    catch (Exception e)
                    {
                        updateConcertList(_websiteConcerts, _databaseConcerts);
                        await Task.Delay(600000, stoppingToken);
                    }                
            }
        }

        public List<Concert> getCurrentConcertList()
        {
            return _concertService.Concerts.Where(c => DateTime.Compare(c.start_date, DateTime.Now) >= 0).ToList();
        }


        public void updateConcertList(List<Concert> websiteConcerts, List<Concert> databaseConcerts)
        {
            var sameId = false;

            foreach (var webConcert in websiteConcerts)
            {
                foreach (var dbConcert in databaseConcerts)

                {
                    if (webConcert.id == dbConcert.id)
                    {
                        sameId = true;
                        break;
                    }
                }
                //CASE 1: Concert is in both databaseConcerts and websiteConcerts --> update info inside Concert looking for changes
                if (sameId)
                {                    
                    Concert concertUpdate = _concertService.Concerts.AsNoTracking().FirstOrDefault(c => c.id == webConcert.id);
                    _concertService.Update(webConcert);
                    _concertService.SaveChanges();
                    _logger.LogInformation("Concert with ID: " + webConcert.id + " updated");
                    sameId = false;
                }

                //CASE 2: Concert in websiteConcerts but not in databaseConcerts --> add Concert to databaseConcerts
                else
                {                    
                    _concertService.Add(webConcert);
                    _concertService.SaveChanges();
                    _logger.LogInformation("Concert with ID: " + webConcert.id + " added");
                }
            }

            //CASE 3: Concert in databaseConcerts but not in websiteConcerts --> delete Concert from databaseConcerts
            List<Concert> except = databaseConcerts.Except(websiteConcerts).ToList();
            foreach (var item in except)
            {
                _logger.LogInformation("item id: " + item.id);
                _concertService.Attach(item);
                _concertService.Remove(item);                
                _concertService.SaveChanges();
                _logger.LogInformation("Concert with ID: "+ item.id +" removed");
            }
        }
    }
}
