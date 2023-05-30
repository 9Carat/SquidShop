using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace SquidShopWebApp.API
{
    public class DistanceApi
    {
        private string result;

        public async Task<string> Get(string address)
        {
            var result = await GetResponse(address);
            return result;
        }

        private async Task<string> GetResponse(string address)
        {
            string shippingAddress = address;
            string _address = $"https://www.mapquestapi.com/directions/v2/route?key=9R4LI0SCotTgSGtlwK1OCjqsVqMH0f06&from=Birger+Jarlsgatan+Stockholm&to={shippingAddress}&unit=k";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            string strStart = "\"distance\":";
            string strEnd = ",";
            if (data.Contains(strStart) && data.Contains(strEnd))
            {
                int start = data.IndexOf(strStart, 0) + strStart.Length;
                int end = data.IndexOf(strEnd, start);
                var result = data.Substring(start, end - start) + " km";
                return result;
            }
            else
            {
                return "Error, distance not found";
            }
        }
    }
}
