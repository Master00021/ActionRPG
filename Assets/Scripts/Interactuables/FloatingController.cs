using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class FloatingController : MonoBehaviour
{
    
    internal static event Action<float, float> OnPlayerMovement;

    internal static bool AllowInput { get; set; }

    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;

    private void Update() {
        if (!AllowInput) return;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (_horizontalInput != 0 || _verticalInput != 0) {
            OnPlayerMovement?.Invoke(_horizontalInput, _verticalInput);
        }
    }
}
