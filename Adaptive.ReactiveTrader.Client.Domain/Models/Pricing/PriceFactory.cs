using NewWave.FastTrader.Client.Domain.Instrumentation;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;
using NewWave.FastTrader.Client.Domain.Repositories;
using NewWave.FastTrader.Shared.DTO.Pricing;

namespace NewWave.FastTrader.Client.Domain.Models.Pricing
{
    internal class PriceFactory : IPriceFactory
    {
        private readonly IExecutionRepository _executionRepository;
        private readonly IPriceLatencyRecorder _priceLatencyRecorder;

        public PriceFactory(IExecutionRepository executionRepository, IPriceLatencyRecorder priceLatencyRecorder)
        {
            _executionRepository = executionRepository;
            _priceLatencyRecorder = priceLatencyRecorder;
        }

        public IPrice Create(PriceDto priceDto, ICurrencyPair currencyPair)
        {
            var bid = new ExecutablePrice(Direction.SELL, priceDto.Bid, _executionRepository);
            var ask = new ExecutablePrice(Direction.BUY, priceDto.Ask, _executionRepository);
            var price = new Price(bid, ask, priceDto.Mid, priceDto.QuoteId, priceDto.ValueDate, currencyPair, priceDto.CreationTimestamp);

            _priceLatencyRecorder.OnReceived(price);

            return price;
        }
    }
}