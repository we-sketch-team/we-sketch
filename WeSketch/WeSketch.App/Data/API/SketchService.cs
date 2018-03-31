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
        private IAPI proxy;

        private static SketchService instance;

        private SketchService()
        {
            proxy = new ProxyService();
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
            IAPI service = instance.proxy;  

            return service;            
        }
    }
}
