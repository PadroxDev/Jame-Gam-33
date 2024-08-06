using Acelab.Modules.Audio;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class Altar : MonoBehaviour
    {
        [BoxGroup("Glasses"), SerializeField] private SO_GlassesBase _glassesToUnlock;
        [BoxGroup("Glasses"), SerializeField] private Transform _glassesHolder;
        [BoxGroup("Audio"), Range(0, 1), SerializeField] private float _sfxVol;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Light _light;
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

            _glassesPrefab.transform.DOScale(0, 0.3f).SetEase(Ease.InBack);
            _particleSystem.Stop();
            _light.DOIntensity(0, 1.3f);

            AudioController.Instance.PlayAudio(Acelab.Modules.Audio.AudioType.SFX_GlassesDiscovered, _glassesPrefab.transform.position,
                _sfxVol);
            Destroy(_glassesPrefab, 1);

            GlassesWheel.Instance.UnlockGlasses(_glassesToUnlock, true);
        }
    }
}
