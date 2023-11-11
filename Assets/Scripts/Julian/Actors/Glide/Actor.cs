using static Unity.Mathematics.math;
using UnityEngine;
using System;

internal sealed class Actor : MonoBehaviour
{

    [SerializeField] private bool _playOnStart;

    [SerializeField] private GlideConfiguration _glideConfiguration;
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private PlayerGlide _playerGlide; 
    [SerializeField] private GlideStatus _glideStatus;   

    private bool _isPlaying;

    private void Awake() {
        if (_playOnStart) {
            Play();
        }
    }

    private void Update() {
        _playerGlide.ImpulsePlayer(_glideStatus.Current);
        _playerGlide.GlidePlayer(_glideStatus.Current, _glideStatus.Previous);
        _playerGlide.BackPlayerToNormal(_glideStatus.Current);
    }

    private void OnDisable() {
        _playerGlide?.Disable();
    }

    private void Play() {
        if (_isPlaying) return;
        _isPlaying = true;

        _playerGlide = new(_glideConfiguration, _glideStatus, _playerRigidBody, _groundLayerMask);        
    }

}
