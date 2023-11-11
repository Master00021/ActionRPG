using UnityEngine;
using System;

internal sealed class PlayerGliderInput : MonoBehaviour
{

    internal static event Action<float, float> OnPlayerMovement;

    internal static bool AllowInput { get; set; }

    [SerializeField] private bool _enableInputOnAwake;
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;

    private void Awake() {
        if (_enableInputOnAwake) {
            AllowInput = true;
        }
    }

    private void Update() {
        if (!AllowInput) return;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (AllowInput) {
            OnPlayerMovement?.Invoke(_horizontalInput, _verticalInput);
        }
    }   

}
