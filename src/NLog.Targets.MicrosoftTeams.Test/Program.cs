using System;

namespace NLog.Targets.MicrosoftTeams.Test
{
    class Program
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            _logger.Info("Test Info Message.");
            _logger.Fatal(new ArgumentException("42"), "Fatal!");
            try
            {
                throw new Exception("This a test exception");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            Console.ReadKey();
        }
    }
}
