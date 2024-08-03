using Acelab.Core.Utility;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mini_Jame_Gam_3
{
    public class GlassesWheel : Singleton<GlassesWheel>
    {
        private const string WHEEL_OPENED = "Opened";

        [BoxGroup("References"), SceneObjectsOnly, SerializeField] private GlassesWheelButton[] _wheelButtons;
        [BoxGroup("References"), SceneObjectsOnly, SerializeField] private GlassesWheelCenter _wheelCenter;
        private Animator _animator;
        private int _currentIndex;
        private GameObject _lastSelectedGlassesButton;

        protected override void Awake() {
            base.Awake();
            _animator = GetComponent<Animator>();
            _currentIndex = 0;
        }

        public void OpenWheel() {
            EventSystem.current.SetSelectedGameObject(_lastSelectedGlassesButton);
            if(_lastSelectedGlassesButton.TryGetComponent(out GlassesWheelButton glassesWheelButton)) {
                _wheelCenter.UpdateGlassesData(glassesWheelButton.GlassesData);
            }
            _animator.SetBool(WHEEL_OPENED, true);
        }

        public void CloseWheel() {
            _animator.SetBool(WHEEL_OPENED, false);
            _lastSelectedGlassesButton = EventSystem.current.currentSelectedGameObject;
        }

        public void UnlockGlasses(SO_GlassesBase glasses) {
            if(_currentIndex >= _wheelButtons.Length) {
                Debug.LogWarning($"Trying to unlock more than {_wheelButtons.Length} glasses in the Wheel !");
                return;
            }

            GlassesWheelButton wheelBtn = _wheelButtons[ _currentIndex ];
            wheelBtn.Initialize(glasses, _currentIndex == 0);

            if(_currentIndex == 0) {
                _lastSelectedGlassesButton = wheelBtn.gameObject;
                _wheelCenter.UpdateGlassesData(glasses);
            }

            _currentIndex++;
        }
    }
}
