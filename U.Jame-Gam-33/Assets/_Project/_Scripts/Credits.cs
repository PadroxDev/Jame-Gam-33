using Acelab.Modules.SceneManagment;
using DG.Tweening;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private RectTransform _credits;
        [SerializeField] private float _targetHeight;
        [SerializeField] private float _duration = 3f;

        private void Start() {
            _credits.DOMoveY(_targetHeight, _duration).OnComplete(() => {
                SceneManager.OpenSceneWithArgs<MainMenuController, MainMenuArgs>();
            });
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape))
                SceneManager.OpenSceneWithArgs<MainMenuController, MainMenuArgs>();
        }
    }
}
