using System.Collections;
using UnityEngine;

internal sealed class PushableObject : MonoBehaviour {

    [SerializeField] private PushableConfiguration _pushableConfiguration;
    [SerializeField] private Rigidbody _rigidBody;

    private Vector3 _directionToLaunch;

    private float _accumulativeForce = 25.0f;
    private float _damageIncrementByMass;
    private float _timeToAccumulate;
    private bool _coroutineActive;
    private bool _launched;
    private float _damage;
    private int _hits = 0;

    private void Awake() {
        _damageIncrementByMass = _rigidBody.mass * Mathf.Log10(_rigidBody.mass);
        _timeToAccumulate = _pushableConfiguration.TimeToAccumulate;
    }    

    private void Update() {       
        if (_launched) _damage = _rigidBody.velocity.z + _rigidBody.mass / 2.0f;
        else _damage = 0.0f;
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable)) {
            damageable.Damage(_damage);
            Destroy(gameObject);
            }

        if (other.gameObject.CompareTag("Attack")) {
            _rigidBody.isKinematic = true;
            _launched = true;

            var actor = other.gameObject.GetComponent<Rigidbody>();

            if (_hits <= _pushableConfiguration.HitsLimit) {
                _accumulativeForce += _accumulativeForce * 0.25f;
                _hits++;
            }

            _directionToLaunch = actor.GetComponent<Transform>().forward;

            if (!_coroutineActive) {
                _coroutineActive = true;
                StartCoroutine(CO_TimeToAccumulateForce());            
            }            
        }   
    }

    private IEnumerator CO_TimeToAccumulateForce() {
        while (_timeToAccumulate > 0.0f) {
            _timeToAccumulate -= Time.deltaTime;
            yield return null;
        }
        _rigidBody.isKinematic = false;

        var launchVelocity = new Vector3(0.0f, 
                                         _pushableConfiguration.RiseForce * _accumulativeForce + _damageIncrementByMass, 
                                         _pushableConfiguration.ForwardForce * _accumulativeForce + _damageIncrementByMass);

        launchVelocity += _directionToLaunch;

        _rigidBody.AddForce(launchVelocity, ForceMode.Impulse); 
    }
    
}
