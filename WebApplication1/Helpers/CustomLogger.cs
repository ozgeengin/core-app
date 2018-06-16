using System.IO;
using System.Reflection;
using System.Xml;
using log4net;

namespace WebApplication1.Helpers
{
    public static class CustomLogger
    {
        private static ILog _logger;

        public static ILog Log()
        {
            return _logger;
        }

        public static void Configure()
        {
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4NetConfig["log4net"]);
            _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
