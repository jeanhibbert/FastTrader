﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Reactive;

namespace NewWave.FastTrader.Client.Domain.Transport
{
    internal interface IConnection
    {
        IObservable<ConnectionInfo> StatusStream { get; }
        IObservable<Unit> Initialize();
        string Address { get; }
        IHubProxy ReferenceDataHubProxy { get; }
        IHubProxy PricingHubProxy { get; }
        IHubProxy ExecutionHubProxy { get; }
        IHubProxy BlotterHubProxy { get; }
    }
 }