using UnityEngine.Events;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{

    public string _tagToDetect;
    public UnityEvent<Collider> _onTriggerEnter;
    public UnityEvent<Collider> _onTriggerStay;
    public UnityEvent<Collider> _onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToDetect))
        {
            _onTriggerEnter?.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tagToDetect))
        {
            _onTriggerStay?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagToDetect))
        {
            _onTriggerExit?.Invoke(other);
        }
    }
}
