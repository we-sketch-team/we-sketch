using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeSketch.Server
{
    public static class Logger
    {
        public static ILoggable Target { get; set; }

        public static void Log(string message)
        {
            if (Target == null) return;
            Target.Log(message);
        }
    }
}
