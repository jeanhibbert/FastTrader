using System.Threading.Tasks;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Server.Execution
{
    public interface IExecutionService
    {
        Task<TradeDto> Execute(TradeRequestDto tradeRequest, string username);
    }
}