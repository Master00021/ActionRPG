using System;
using UnityEngine;

internal sealed class Glide {

    private GlideConfiguration _glideConfiguration;
    private GlideStatus _glideStatus;
    private Rigidbody _playerRigidBody;
    private LayerMask _groundLayerMask;
    private float _horizontalInput;
    private float _verticalInput;

    public Glide(GlideConfiguration glideConfiguration, GlideStatus glideStatus, Rigidbody rigidBody, LayerMask groundLayerMask) {
        _glideConfiguration = glideConfiguration;
        _glideStatus = glideStatus;
        _playerRigidBody = rigidBody;
        _groundLayerMask = groundLayerMask;

        _glideStatus.UpdateGlideState(GlideState.Normal, GlideState.Normal);

        GlideInput.OnPlayerMovement += GetPlayerAxis;
        GlideObject.OnPlayerEntry += UpdateToImpulseState;
        GlideObject.OnPlayerExit += UpdateToGlideState;
    }

    internal void Disable() {
        GlideInput.OnPlayerMovement -= GetPlayerAxis;
        GlideObject.OnPlayerEntry -= UpdateToImpulseState;
        GlideObject.OnPlayerExit -= UpdateToGlideState;
    }

    internal void GetPlayerAxis(float horizontalInput, float verticalInput) {
        _horizontalInput = horizontalInput;
        _verticalInput = verticalInput;
    }

    internal void ImpulsePlayer(GlideState currentGlideState) {
        if (currentGlideState != GlideState.Impulse) return;

        _playerRigidBody.useGravity = false;
        _playerRigidBody.AddForce(_playerRigidBody.transform.up 
                                  * _glideConfiguration.ImpulseForce 
                                  * Time.deltaTime, ForceMode.Impulse);
    }

    internal void GlidePlayer(GlideState currentGlideState, GlideState previousGlideState) {
        if (currentGlideState != GlideState.Glide 
            && previousGlideState != GlideState.Impulse) return;

        _playerRigidBody.useGravity = true;
        _playerRigidBody.drag = 5.0f;

        var moveDirection = new Vector3(_horizontalInput, 0.0f, _verticalInput);
        _playerRigidBody.transform.position += new Vector3(moveDirection.x * _glideConfiguration.GlideSpeed, 0.0f, moveDirection.z * _glideConfiguration.GlideSpeed) * Time.deltaTime; 

        _glideConfiguration.NormalPlayer = Physics.Raycast(_playerRigidBody.transform.position, 
                                                           -_playerRigidBody.transform.up, 1.5f, _groundLayerMask);

        if (_glideConfiguration.NormalPlayer) {
            UpdateToNormalState();
        }       
    }

    internal void BackPlayerToNormal(GlideState currentGlideState) {
        if (currentGlideState != GlideState.Normal) return;

        _playerRigidBody.drag = 0.0f;
    }    

    private void UpdateToImpulseState() {
        _glideStatus.UpdateGlideState(GlideState.Impulse, GlideState.Normal);
    }

    private void UpdateToGlideState() {
        _glideStatus.UpdateGlideState(GlideState.Glide, GlideState.Impulse);
    }

    private void UpdateToNormalState() {
        _glideStatus.UpdateGlideState(GlideState.Normal, GlideState.Glide);
    }
    
}
