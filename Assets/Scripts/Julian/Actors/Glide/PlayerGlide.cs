using UnityEngine;

internal sealed class PlayerGlide
{

    private GlideConfiguration _glideConfiguration;
    private Rigidbody _playerRigidBody;
    private float _horizontalInput;
    private float _verticalInput;

    public PlayerGlide(GlideConfiguration glideConfiguration, Rigidbody rigidBody) {
        _glideConfiguration = glideConfiguration;
        _playerRigidBody = rigidBody;

        PlayerGliderInput.OnPlayerMovement += GetPlayerAxis;
    }

    internal void Disable() {
        PlayerGliderInput.OnPlayerMovement -= GetPlayerAxis;
    }

    internal void GetPlayerAxis(float horizontalInput, float verticalInput) {
        _horizontalInput = horizontalInput;
        _verticalInput = verticalInput;
    }

    internal void Impulse() {
        // LOGICA DE IMPULSO
        Debug.Log("Impulsando...");

        _playerRigidBody.useGravity = false;
        _playerRigidBody.AddForce(_playerRigidBody.transform.up * _glideConfiguration.ImpulseForce * Time.deltaTime, ForceMode.Impulse);
    }

    internal void Glide() {
        // LOGICA DE PLANEAR 
        Debug.Log("Planeando...");
        _playerRigidBody.useGravity = true;
        _playerRigidBody.drag = 5.0f;
    }

    internal void Normal() {
        Debug.Log("Normalidad");
        _playerRigidBody.drag = 0.0f;
    }    
    
}
