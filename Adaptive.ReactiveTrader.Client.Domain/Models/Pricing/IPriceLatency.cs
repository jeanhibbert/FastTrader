﻿namespace NewWave.FastTrader.Client.Domain.Models.Pricing
{
    public interface IPriceLatency
    {
        double ServerToClientMs { get; }
        double UiProcessingTimeMs { get; }
        double TotalLatencyMs { get; }
        void DisplayedOnUi();
        void ReceivedInGuiProcess();
    }
}