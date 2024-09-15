using System;
using System.Collections.Generic;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;

namespace NewWave.FastTrader.Client.Domain.Repositories
{
    public interface IReferenceDataRepository
    {
        IObservable<IEnumerable<ICurrencyPairUpdate>> GetCurrencyPairsStream();
    }
}
