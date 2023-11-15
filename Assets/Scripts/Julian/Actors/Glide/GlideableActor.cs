using static Unity.Mathematics.math;
using UnityEngine;
using System;

internal sealed class GlideableActor : MonoBehaviour
{

    [SerializeField] private bool _playOnStart;

    [SerializeField] private GlideConfiguration _glideConfiguration;
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private GlideStatus _glideStatus;   
    [SerializeField] private Glide _glide; 

    private bool _isPlaying;

    private void Awake() {
        if (_playOnStart) {
            Play();
        }
    }

    private void OnDisable() {
        _glide?.Disable();
    }

    private void Update() {
        _glide.ImpulsePlayer(_glideStatus.Current);
        _glide.GlidePlayer(_glideStatus.Current, _glideStatus.Previous);
        _glide.BackPlayerToNormal(_glideStatus.Current);
    }

    private void Play() {
        if (_isPlaying) return;
        _isPlaying = true;

        _glide = new(_glideConfiguration, _glideStatus, _playerRigidBody, _groundLayerMask);  
    }

}
