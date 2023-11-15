using UnityEngine;

internal sealed class Trap {

    private TrapConfiguration _trapConfiguration;
    private float _deactivateTime;
    private Rigidbody _rigidBody;
    private bool _selfActivate;
    private GameObject _miniBoss;
    private Transform _trap;
    private float _stunTime;
    
    internal Trap(TrapConfiguration trapConfiguration, Rigidbody rigidBody) {
        _trapConfiguration = trapConfiguration;
        _rigidBody = rigidBody;

        _stunTime = _trapConfiguration.BossStunTime;

        TrapObject.OnActorDetected += Activate;
    }

    private void Activate(GameObject miniBoss, GameObject trap) {
        if (_deactivateTime > 0.0f) return;
        _trap = trap.transform;
        _miniBoss = miniBoss;
        _miniBoss.GetComponent<Rigidbody>().isKinematic = true;
        _selfActivate = true;
    }

    internal void Explode() {
        if (!_selfActivate) return;
        
        _deactivateTime = _trapConfiguration.DeactivateTime;

        var explisionPosition = new Vector3(_trap.position.x, 
                                            _trap.position.y + _trapConfiguration.ExplosionOffset, 
                                            _trap.position.z);

        _rigidBody.AddExplosionForce(_trapConfiguration.ExplosionForce, 
                                     explisionPosition, 
                                     _trapConfiguration.ExplosionRadius, 
                                     _trapConfiguration.UpWardsModifier, 
                                     ForceMode.VelocityChange);
        
        _selfActivate = false;
    }

    internal void BossStun() {
        if (!_miniBoss || _miniBoss.GetComponent<Rigidbody>().isKinematic == false) return;
        
        _stunTime -= Time.deltaTime;
        if (_stunTime <  0.0f) {
            _miniBoss.GetComponent<Rigidbody>().isKinematic = false;
            _stunTime = _trapConfiguration.BossStunTime;
        }
    }

    internal void ActivateTrap() {
        if (!_trap) return;
        _deactivateTime -= Time.deltaTime;
        if (_deactivateTime < 0.0f) {
            _deactivateTime = 0.0f;
        }
    }

    internal void Disable() {
        TrapObject.OnActorDetected -= Activate;
    }

}
