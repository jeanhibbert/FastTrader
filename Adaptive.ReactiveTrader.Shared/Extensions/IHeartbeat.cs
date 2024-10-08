﻿namespace NewWave.FastTrader.Shared.Extensions
{
    public interface IHeartbeat<out T>
    {
        bool IsHeartbeat { get; }
        T Update { get; }
    }
}