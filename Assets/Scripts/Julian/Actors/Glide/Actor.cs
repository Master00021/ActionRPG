using static Unity.Mathematics.math;
using UnityEngine;
using System;

internal sealed class Actor : MonoBehaviour
{

    [SerializeField] private bool _playOnStart;

    [SerializeField] private GlideConfiguration _glideConfiguration;
    [SerializeField] private Rigidbody _playerRigidBody;
    [SerializeField] private PlayerGlide _playerGlide; 
    [SerializeField] private GlideStatus _glideStatus;   

    private bool _isPlaying;

    private void Awake() {
        if (_playOnStart) {
            Play();
        }
    }

    private void Update() {
        if (_glideStatus.Current == GlideState.Impulsed) {
            _playerGlide.Impulse();
        }

        if (_glideStatus.Current == GlideState.Gliding && _glideStatus.Previous == GlideState.Impulsed) {
            _playerGlide.Glide();
        }

        if (_glideStatus.Current == GlideState.Gliding && _playerRigidBody.velocity.y == 0.0f) {
            GroundPlayer();
        }

        if (_glideStatus.Current == GlideState.None && _glideStatus.Previous == GlideState.Gliding) {
            _playerGlide.Normal();
        }
    }

    private void OnDisable() {
        Glide.OnPlayerEntry -= ImpulsePlayer;
        Glide.OnPlayerExit -= GlidePlayer;
        _playerGlide?.Disable();
    }

    private void Play() {
        if (_isPlaying) return;
        _isPlaying = true;

        _playerGlide = new(_glideConfiguration, _playerRigidBody);

        Glide.OnPlayerEntry += ImpulsePlayer;
        Glide.OnPlayerExit += GlidePlayer;
    }

    private void ImpulsePlayer() {
        _glideStatus.UpdateGlideState(GlideState.Impulsed, GlideState.None);
    }

    private void GlidePlayer() {
        _glideStatus.UpdateGlideState(GlideState.Gliding, GlideState.Impulsed);
    }

    private void GroundPlayer() {
        _glideStatus.UpdateGlideState(GlideState.None, GlideState.Gliding);
    }

}
