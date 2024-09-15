﻿using System;
using System.Threading.Tasks;
using NewWave.FastTrader.Server.Transport;
using NewWave.FastTrader.Shared.DTO.Pricing;
using log4net;

namespace NewWave.FastTrader.Server.Pricing
{
    public class PricePublisher : IPricePublisher
    {
        private readonly IContextHolder _contextHolder;
        private static readonly ILog Log = LogManager.GetLogger(typeof(PricePublisher));
        private long _totalUpdatesPublished;
        
        public PricePublisher(IContextHolder contextHolder)
        {
            _contextHolder = contextHolder;
        }

        public async Task Publish(PriceDto price)
        {
            var context = _contextHolder.PricingHubClient;
            if (context == null) return;

            _totalUpdatesPublished++;
            var groupName = string.Format(PricingHub.PriceStreamGroupPattern, price.Symbol);
            try
            {
                await context.Group(groupName).OnNewPrice(price);
                if (Log.IsDebugEnabled)
                {
                    Log.DebugFormat("Published price to group {0}: {1}", groupName, price);
                }
            }
            catch (Exception e)
            {
                Log.Error(string.Format("An error occured while publishing price to group {0}: {1}", groupName, price), e);
            }
        }

        public long TotalPricesPublished { get { return _totalUpdatesPublished; } }
    }
}