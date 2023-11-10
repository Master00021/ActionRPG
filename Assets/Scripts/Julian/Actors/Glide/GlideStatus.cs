using System;
using UnityEngine;

internal enum GlideState {
    None,
    Impulsed,
    Gliding
}

internal sealed class GlideStatus : MonoBehaviour
{

    internal static event Action<GlideState, GlideState> OnGlideStateChanged;

    internal GlideState Current;
    internal GlideState Previous;

    internal void UpdateGlideState(GlideState newGlideState, GlideState previousGlideState) {
        previousGlideState = Current;
        Previous = previousGlideState;
        Current = newGlideState;

        OnGlideStateChanged?.Invoke(newGlideState, previousGlideState);
    }
}
