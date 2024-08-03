using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mini_Jame_Gam_3
{
    public class GlassesWheelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private const string HOVERED_ANIM = "Hovered";

        [BoxGroup("References"), SerializeField] private GlassesManager _glassesManager;
        [BoxGroup("References"), SerializeField] private Image _icon;
        [BoxGroup("References"), SerializeField] private GlassesWheelCenter _center;
        private Animator _animator;
        private Button _btn;
        private SO_GlassesBase _glasses;

        public SO_GlassesBase GlassesData { get { return _glasses; } }

        private void Awake() {
            _animator = GetComponent<Animator>();
            _btn = GetComponent<Button>();
        }

        public void Initialize(SO_GlassesBase glasses, bool first) {
            _glasses = glasses;
            _btn.interactable = true;
            _icon.sprite = glasses.Icon;

            if (!first) return;
            _glassesManager.EquipGlasses(_glasses);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (!_btn.interactable) return;
            _animator.SetBool(HOVERED_ANIM, true);
            _center.FadeUpdateGlassesData(_glasses);
        }

        public void OnPointerExit(PointerEventData eventData) {
            if (!_btn.interactable) return;
            _animator.SetBool(HOVERED_ANIM, false);
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!_btn.interactable) return;
            _glassesManager.EquipGlasses(_glasses);
        }
    }
}
