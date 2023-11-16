using UnityEngine;

internal sealed class Destroyable : MonoBehaviour {

    [SerializeField] private int _health;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Attack")) return;
        
        
    }
    
}
