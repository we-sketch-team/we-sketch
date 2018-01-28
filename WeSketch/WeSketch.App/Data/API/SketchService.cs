using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using WeSketch.App.Data.Shapes;
using Microsoft.AspNet.SignalR.Client;

namespace WeSketch.App.Data.API
{
    public class SketchService
    {
        private IAPI api;
        private IAPI proxy;

        public static bool HasInternetConnection { get; set; }

        private static SketchService instance;

        private SketchService()
        {
            api = new ApiService();
            proxy = new ProxyService();
            HasInternetConnection = true;
        }


        private static SketchService GetInstance()
        {
            if (instance == null)
            {
                instance = new SketchService();
            }

            return instance;
        }

        public static IAPI GetService()
        {
            var instance = GetInstance();
            IAPI service;

            if (HasInternetConnection)
                service = instance.api;
            else
                service = instance.proxy;

            return service;            
        }
    }
}
