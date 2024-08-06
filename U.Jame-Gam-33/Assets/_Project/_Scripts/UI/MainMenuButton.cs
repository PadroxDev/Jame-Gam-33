using Acelab.Modules.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mini_Jame_Gam_3
{
    public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        public void Exit() {
            Application.Quit();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            _animator.SetBool("Hovered", true);
            AudioController.Instance.PlayAudio(Acelab.Modules.Audio.AudioType.SFX_MMHover);
        }

        public void OnPointerExit(PointerEventData eventData) {
            _animator.SetBool("Hovered", false);
        }

        public void OnPointerClick(PointerEventData eventData) {
            AudioController.Instance.PlayAudio(Acelab.Modules.Audio.AudioType.SFX_MMButtonClick);
        }
    }
}
