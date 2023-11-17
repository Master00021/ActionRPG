using UnityEngine;

public interface ITrap {
    
    public void Stun(Rigidbody rigidbody, float stunTime);
    public void Reactivate(TrapConfiguration configuration, GameObject trap, float timeToReactivate);
}
