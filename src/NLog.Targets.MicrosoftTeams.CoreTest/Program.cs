using System;

namespace NLog.Targets.MicrosoftTeams.CoreTest
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
