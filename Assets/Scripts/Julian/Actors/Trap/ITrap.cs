using UnityEngine;

public interface ITrap {
    
    public void Stun(float stunTime);
    public void Reactivate(GameObject trap, float timeToReactivate);
}
