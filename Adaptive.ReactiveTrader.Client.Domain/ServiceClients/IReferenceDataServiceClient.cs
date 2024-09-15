using System;
using System.Collections.Generic;
using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Client.Domain.ServiceClients
{
    internal interface IReferenceDataServiceClient
    {
        IObservable<IEnumerable<CurrencyPairUpdateDto>> GetCurrencyPairUpdatesStream();
    }
}