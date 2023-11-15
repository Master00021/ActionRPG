using UnityEngine;

internal sealed class GlideObject : MonoBehaviour {

    [SerializeField] private Transform _maxHeight;

    private void OnTriggerEnter(Collider player) {
        player.gameObject.TryGetComponent<IGlide>(out var glidePlayer);
        glidePlayer.Impulse(_maxHeight);
    }

}
