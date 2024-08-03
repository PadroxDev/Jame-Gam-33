using Sirenix.OdinInspector;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class Altar : MonoBehaviour
    {
        [BoxGroup("Glasses"), SerializeField] private SO_GlassesBase _glassesToUnlock;
        [BoxGroup("Glasses"), SerializeField] private Transform _glassesHolder;
        private GameObject _glassesPrefab;
        private bool _given;

        private void Awake() {
            _given = false;
        }

        private void Start() {
            _glassesPrefab = Instantiate(_glassesToUnlock.GlassesPrefab, _glassesHolder);
        }

        private void OnTriggerEnter(Collider other) {
            if (_given) return;

            GlassesManager glassesManager = other.GetComponentInParent<GlassesManager>();
            if (glassesManager == null) return;
            _given = true;

            Destroy(_glassesPrefab);

            GlassesWheel.Instance.UnlockGlasses(_glassesToUnlock);
        }
    }
}
