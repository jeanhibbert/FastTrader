﻿using System;
using System.Diagnostics;
using NewWave.FastTrader.Client.Domain.Models.Pricing;

namespace NewWave.FastTrader.Client.Domain.Instrumentation
{
    public class PriceLatencyRecorder : IPriceLatencyRecorder
    {
        private readonly Process _currentProcess;
        private readonly Histogram _uiLatency;
        private readonly Histogram _serverLatency;
        private readonly Histogram _combinedLatency;
        private readonly object _histogramLock = new object();
        private TimeSpan _lastProcessTime;

        public PriceLatencyRecorder()
        {
            _currentProcess = Process.GetCurrentProcess();
            _lastProcessTime = _currentProcess.UserProcessorTime;

            _uiLatency = GetHistogram();
            _serverLatency = GetHistogram();
            _combinedLatency = GetHistogram();
        }

        public void OnRendered(IPrice price)
        {
            var priceLatency = price as IPriceLatency;
            if (priceLatency != null)
            {
                priceLatency.DisplayedOnUi();
                _uiLatency.AddObservation((long)priceLatency.UiProcessingTimeMs);
                _combinedLatency.AddObservation((long)priceLatency.TotalLatencyMs);
            }
        }

        public void OnReceived(IPrice price)
        {
            var priceLatency = price as IPriceLatency;
            if (priceLatency != null)
            {
                priceLatency.ReceivedInGuiProcess();
                lock (_histogramLock)
                {
                    _serverLatency.AddObservation((long) priceLatency.ServerToClientMs);
                }
            }
        }

        public Statistics CalculateAndReset()
        {
            var stats = new Statistics();

            lock (_histogramLock)
            {
                stats.RenderedCount = _uiLatency.Count;
                stats.ReceivedCount = _serverLatency.Count;
                stats.ServerLatencyMax = _serverLatency.Max;
                stats.UiLatencyMax = _uiLatency.Max;
                stats.TotalLatencyMax = _combinedLatency.Max;
                stats.Histogram = _combinedLatency.ToString();
                
                var currentProcessTime = _currentProcess.UserProcessorTime;
                stats.ProcessTime = currentProcessTime.Subtract(_lastProcessTime);
                _lastProcessTime = currentProcessTime;


                _uiLatency.Clear();
                _combinedLatency.Clear();
                _serverLatency.Clear();
            }

            return stats;
        }

        private Histogram GetHistogram()
        {
            var intervals = new long[13];
            var intervalUpperBound = 1L;
            for (var i = 0; i < intervals.Length - 1; i++)
            {
                intervalUpperBound *= 2;
                intervals[i] = intervalUpperBound;
            }

            intervals[intervals.Length - 1] = long.MaxValue;
            return new Histogram(intervals);
        }
    }
}