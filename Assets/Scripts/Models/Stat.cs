using System;
using UnityEngine;

/// <summary>A value between 0 and Max that notifies listeners when it changes.</summary>
public class Stat
{
    public float Current { get; private set; }
    public float Max { get; private set; }

    /// <summary>Raised whenever Current or Max changes: (current, max).</summary>
    public event Action<float, float> Changed;

    public Stat(float max)
    {
        Max = max;
        Current = max;
    }

    public void Set(float value)
    {
        float clamped = Mathf.Clamp(value, 0f, Max);
        if (Mathf.Approximately(clamped, Current)) return;

        Current = clamped;
        Changed?.Invoke(Current, Max);
    }

    public void Add(float delta) => Set(Current + delta);

    /// <summary>Changes the maximum. Current is clamped down if it is above the new maximum.</summary>
    public void SetMax(float max)
    {
        Max = Mathf.Max(0f, max);
        Current = Mathf.Min(Current, Max);
        Changed?.Invoke(Current, Max);
    }
}
