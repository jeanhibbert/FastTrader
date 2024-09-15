using System;
using NewWave.FastTrader.Shared.DTO.Pricing;

namespace NewWave.FastTrader.Client.Domain.ServiceClients
{
    internal interface IPricingServiceClient
    {
        IObservable<PriceDto> GetSpotStream(string currencyPair);
    }
}