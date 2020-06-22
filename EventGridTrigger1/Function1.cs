// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Text;

public class EventItem
{
    public long DeviceId { get; set; }
    public string Image { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Text { get; set; }
    public string Name { get; set; }
}

public class ThingItem
{
    public long Thingid { get; set; }
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Image { get; set; }
    public string Text { get; set; }
    public string Status { get; set; }
}



namespace EventGridTrigger1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log)
        {
 
            log.LogInformation(eventGridEvent.Data.ToString());

            var jsonEvent = JsonSerializer.Deserialize<EventItem>(eventGridEvent.Data.ToString());

            var ThingId = jsonEvent.DeviceId + 500;
            log.LogInformation("Thing Id: " + ThingId);

            ThingItem thingItem = new ThingItem
            {
                Thingid = jsonEvent.DeviceId + 500,
                Latitude = jsonEvent.Latitude,
                Longitude = jsonEvent.Longitude,
                Image = jsonEvent.Image,
                Text = jsonEvent.Text,
                Name = jsonEvent.Name
            };

            var jsonThing = JsonSerializer.Serialize<ThingItem>(thingItem);

            log.LogInformation("Thing >>>" + jsonThing.ToString());

            string logappUrl = "https://prod-55.northeurope.logic.azure.com:443/workflows/c815fd9d3f73440889a7de71e5130933/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=6elFcQJB3hEHYSHTy-HXaoP9CBQFIdngKRU35IrmA4w";

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(
                    logappUrl,
                    new StringContent(jsonThing, Encoding.UTF8, "application/json")).Result;

                log.LogInformation("Response >>>" + response.StatusCode); 
            }

        }
    }
}
