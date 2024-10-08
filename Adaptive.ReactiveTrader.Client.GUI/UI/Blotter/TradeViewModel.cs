﻿using System;
using System.Globalization;
using NewWave.FastTrader.Client.Domain.Models;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Shared.UI;
using PropertyChanged;

namespace NewWave.FastTrader.Client.UI.Blotter
{
    [AddINotifyPropertyChangedInterface]
    public class TradeViewModel : ViewModelBase, ITradeViewModel
    {
        private readonly bool _isStowTrade;
        private bool _isStowTradePropertyRead;

        public TradeViewModel(ITrade trade, bool isStowTrade)
        {
            _isStowTrade = isStowTrade;
            TradeId = trade.TradeId.ToString(CultureInfo.InvariantCulture);
            CurrencyPair = trade.CurrencyPair.Substring(0, 3) + " / " + trade.CurrencyPair.Substring(3, 3);
            Direction = trade.Direction;
            Notional = trade.Notional.ToString("N0", CultureInfo.InvariantCulture) + " " + trade.DealtCurrency;
            SpotRate = trade.SpotRate;
            TradeDate = trade.TradeDate;
            TradeStatus = trade.TradeStatus == Domain.Models.Execution.TradeStatus.Done ? "Done" : "REJECTED";
            TraderName = trade.TraderName;
            ValueDate = trade.ValueDate;
            DealtCurrency = trade.DealtCurrency;
        }

        public decimal SpotRate { get; private set; }
        public string Notional { get; private set; }
        public Direction Direction { get; private set; }
        public string CurrencyPair { get; private set; }
        public string TradeId { get; private set; }
        public DateTime TradeDate { get; private set; }
        public string TradeStatus { get; private set; }
        public string TraderName { get; private set; }
        public DateTime ValueDate { get; private set; }
        public string DealtCurrency { get; private set; }


        public bool IsNewTrade
        {
            get
            {
                var value = !_isStowTrade && !_isStowTradePropertyRead;
                _isStowTradePropertyRead = true;
                return value;
            }
        }
    }
}
