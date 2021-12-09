using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using ScanningApp.Core.Entity;
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
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        private List<Concert> _retrievedConcertList;
        

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
           
        }

        public IEnumerable<String> EEConcerts { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var httpClient = _httpClientFactory.CreateClient();
                _retrievedConcertList = new List<Concert>();
                var counter = 1;
               // string replacement = String.Replace(" &#8211; ", "-");
                while(true)
                try
                {
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
                            _retrievedConcertList.Add(concert);
                            
                        }
                        counter++;
                    }
                catch(Exception e)
                {
                        foreach (var item in _retrievedConcertList)
                        {
                            _logger.LogInformation(item.title);
                        }
                        break;
                    }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
