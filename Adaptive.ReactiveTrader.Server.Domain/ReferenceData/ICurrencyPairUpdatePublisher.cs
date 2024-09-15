using System.Threading.Tasks;
using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Server.ReferenceData
{
    public interface ICurrencyPairUpdatePublisher
    {
        Task AddCurrencyPair(CurrencyPairDto ccyPair);
        Task RemoveCurrencyPair(CurrencyPairDto ccyPair);
    }
}