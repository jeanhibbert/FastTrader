using System.Diagnostics;

namespace NewWave.FastTrader.Client.Configuration
{
    class UserProvider : IUserProvider
    {
        public string Username
        {
            get
            {
                return "Trader-" + Process.GetCurrentProcess().Id;
            }
        }
    }
}