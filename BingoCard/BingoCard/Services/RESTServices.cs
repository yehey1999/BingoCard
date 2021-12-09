﻿using BingoCard.Models;
using BingoCard.Outputs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BingoCard.Services
{
    class RESTServices
    {
        private static string BaseUrl = "http://www.hyeumine.com";
        private static string UrlGetCard = "/getcard.php";

        private static HttpClient client = null;

        public static HttpClient RestCall
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                }
                return client;
            }
            set { }
        }

        public static async Task<CardOutput> GetCard()
        {
            HttpResponseMessage response = await RestCall.GetAsync(BaseUrl + UrlGetCard + "?bcode=HEelhJos");
            string responseContent = await response.Content.ReadAsStringAsync();
            CardOutput card = JsonConvert.DeserializeObject<CardOutput>(responseContent);
            return card;
        }

    }
}
