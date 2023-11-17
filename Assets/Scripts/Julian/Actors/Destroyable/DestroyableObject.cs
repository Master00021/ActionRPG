using UnityEngine;

internal sealed class DestroyableObject : MonoBehaviour {

    [SerializeField] private int _health;
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Attack")) return;
        
        other.TryGetComponent<IDestroyable>(out var destroyable);
        destroyable.Destroy();
    }
    
}
