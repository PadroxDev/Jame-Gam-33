using Acelab.Modules.SceneManagment;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    [System.Serializable]
    public class LevelData {
        public GameObject GlassesPrefab;
    }

    public class LevelSelector : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Transform _glassesPos;
        [SerializeField] private float _duration = 0.4f;
        [SerializeField] private float _offset = 2f;
        [SerializeField] private LevelData[] _levels;
        private int _currentLevelSelection;
        private bool _canSwap = true;
        private GameObject _currentGlasses;

        private void Awake() {
            _currentLevelSelection = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Start() {
            SpawnGlasses(1f);
        }

        private void SpawnGlasses(float sense) {
            _canSwap = false;
            if (_currentGlasses != null) {
                RemoveGlasses(sense);
            }

            _currentGlasses = Instantiate(_levels[_currentLevelSelection-1].GlassesPrefab,
                _glassesPos.position + Vector3.left * _offset * sense, Quaternion.identity);
            _currentGlasses.transform.localScale = Vector3.zero;

            _currentGlasses.transform.DOMoveX(0 * sense, _duration);
            _currentGlasses.transform.DOScale(1f, _duration).OnComplete(() => {
                _canSwap = true;
            });
        }

        private void RemoveGlasses(float sense) {
            _currentGlasses.transform.DOMoveX(-_offset * -sense, _duration);
            _currentGlasses.transform.DOScale(0f, _duration);
            Destroy(_currentGlasses, _duration + 1f);
        }

        public void PreviousLevel() {
            if (!_canSwap) return;

            _currentLevelSelection--;
            if (_currentLevelSelection < 1)
                _currentLevelSelection = _levels.Length;
            _levelText.SetText("Level #" + _currentLevelSelection);
            SpawnGlasses(-1);
        }

        public void NextLevel() {
            if (!_canSwap) return;

            _currentLevelSelection++;
            if (_currentLevelSelection > _levels.Length) {
                _currentLevelSelection = 1;
            }
            _levelText.SetText("Level #" + _currentLevelSelection);
            SpawnGlasses(1);
        }

        public void BeginLevel() {
            switch (_currentLevelSelection) {
                case 1:
                    SceneManager.OpenSceneWithArgs<Level1Controller, Level1Args>();
                    break;
                case 2:
                    SceneManager.OpenSceneWithArgs<CreditsController, CreditsArgs>();
                    break;
                case 3:
                    SceneManager.OpenSceneWithArgs<CreditsController, CreditsArgs>();
                    break;
            }
        }

        public void Credits() {
            SceneManager.OpenSceneWithArgs<CreditsController, CreditsArgs>();
        }
    }
}
