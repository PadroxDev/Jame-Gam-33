using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class RespawnZone : MonoBehaviour
    {
        [SerializeField] private Transform _respawnPoint;

        private void OnTriggerEnter(Collider other) {
            if (other.transform.parent.tag != "Player") return;

            other.transform.parent.position = _respawnPoint.position;
            other.transform.parent.rotation = _respawnPoint.rotation;

            other.transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
