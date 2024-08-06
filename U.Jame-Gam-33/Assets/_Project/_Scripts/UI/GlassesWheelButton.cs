using Acelab.Modules.Audio;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Mini_Jame_Gam_3
{
    public class GlassesWheelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private const string HOVERED_ANIM = "Hovered";

        [BoxGroup("Audio"), SerializeField, Range(0, 1)] private float _hoverSFXVol = 0.3f;
        [BoxGroup("Audio"), SerializeField, Range(0, 1)] private float _equipSFXVol = 0.4f;

        [BoxGroup("References"), SerializeField] private GlassesManager _glassesManager;
        [BoxGroup("References"), SerializeField] private Image _icon;
        [BoxGroup("References"), SerializeField] private GlassesWheelCenter _center;
        private Animator _animator;
        private Button _btn;
        private SO_GlassesBase _glasses;
        private AudioController _audioController;

        public SO_GlassesBase GlassesData { get { return _glasses; } }

        private void Awake() {
            _animator = GetComponent<Animator>();
            _btn = GetComponent<Button>();
        }

        private void Start() {
            _audioController = AudioController.Instance;
        }

        public void Initialize(SO_GlassesBase glasses, bool instantEquip) {
            _glasses = glasses;
            _btn.interactable = true;
            _icon.sprite = glasses.Icon;

            if (!instantEquip) return;
            _glassesManager.EquipGlasses(_glasses);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (!_btn.interactable) return;
            _animator.SetBool(HOVERED_ANIM, true);
            _center.FadeUpdateGlassesData(_glasses);
            _audioController.PlayAudio(Acelab.Modules.Audio.AudioType.SFX_WheelButtonHover, _hoverSFXVol);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (!_btn.interactable) return;
            _animator.SetBool(HOVERED_ANIM, false);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!_btn.interactable) return;
            _glassesManager.EquipGlasses(_glasses);
            _audioController.PlayAudio(Acelab.Modules.Audio.AudioType.SFX_GlassesEquip, _equipSFXVol);
        }
    }
}
