using UnityEngine;
using UnityEngine.Events;

public class DeathColliderHandler : MonoBehaviour {
    public UnityEvent dead;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if (other.CompareTag("dead")) {
            dead.Invoke();
        }
    }
}