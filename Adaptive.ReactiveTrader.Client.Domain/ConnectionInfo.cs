﻿using NewWave.FastTrader.Client.Domain.Transport;

namespace NewWave.FastTrader.Client.Domain
{
    public class ConnectionInfo
    {
        public ConnectionStatus ConnectionStatus { get; private set; }
        public string Server { get; private set; }

        public ConnectionInfo(ConnectionStatus connectionStatus, string server)
        {
            ConnectionStatus = connectionStatus;
            Server = server;
        }

        public override string ToString()
        {
            return string.Format("ConnectionStatus: {0}, Server: {1}", ConnectionStatus, Server);
        }
    }
}