using System;
using UnityEngine;

internal enum GlideState {
    Normal,
    Impulse,
    Glide
}

internal sealed class GlideStatus : MonoBehaviour
{

    internal static event Action<GlideState, GlideState> OnGlideStateChanged;

    [SerializeField] internal GlideState Current;
    [SerializeField] internal GlideState Previous;

    internal void UpdateGlideState(GlideState newGlideState, GlideState previousGlideState) {
        previousGlideState = Current;
        Previous = previousGlideState;
        Current = newGlideState;

        OnGlideStateChanged?.Invoke(newGlideState, previousGlideState);
    }
}
