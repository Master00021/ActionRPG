using UnityEngine;
using System;

internal sealed class FloatingController : MonoBehaviour
{
    
    internal static event Action<float, float> OnPlayerMovement;

    internal static bool AllowInput { get; set; }

    [Header("Temporal")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _walkSpeed;
    [Header("Datos jugador")]
    [SerializeField] private bool _enableInputOnAwake;
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;

    private Vector3 _moveDirection;
    private float _gravityForce = -9.8F;

    private void Awake() {
        if (_enableInputOnAwake) {
            AllowInput = true;
        }
    }

    private void FixedUpdate() {
        if (!AllowInput) return;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        Walk(_horizontalInput, _verticalInput);

        if (_horizontalInput != 0 || _verticalInput != 0) {
            OnPlayerMovement?.Invoke(_horizontalInput, _verticalInput);
        }
    }

    internal void Walk(float horizontalInput, float verticalInput) {
            // Calcular la direcci�n de movimiento
            print(_characterController.isGrounded);
            if (_characterController.isGrounded) {
                _characterController.attachedRigidbody.useGravity = false;
                _moveDirection = new(horizontalInput, 0.0f, verticalInput);
                _moveDirection = _characterController.transform.TransformDirection(_moveDirection);                
            }
            else {
                //_characterController.attachedRigidbody.useGravity = true;
                _moveDirection.y = _gravityForce * Time.deltaTime;
            }
  
            // Aplicar movimiento
            _characterController.Move(_walkSpeed * Time.deltaTime * _moveDirection);
    }
}
