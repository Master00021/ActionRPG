using UnityEngine;

internal sealed class DestroyerActor : MonoBehaviour, IDestroyable {
    
    public void Destroy() {
        
        Destroy(gameObject);
    }

}
