using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.MicrosoftTeams.Test
{
    class Program
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            //_logger.Info("Info.");
            _logger.Fatal(new ArgumentException("42"), "Fatal!");

            Console.ReadKey();
        }
    }
}
