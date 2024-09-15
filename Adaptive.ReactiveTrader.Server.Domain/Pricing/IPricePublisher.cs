using System.Threading.Tasks;
using NewWave.FastTrader.Shared.DTO.Pricing;

namespace NewWave.FastTrader.Server.Pricing
{
    public interface IPricePublisher
    {
        Task Publish(PriceDto price);
        long TotalPricesPublished { get; }
    }
}