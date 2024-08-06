using Acelab.Modules.SceneManagment;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    [SceneController("Level 2")]
    public class Level2Controller : SceneController<Level2Controller, Level2Args>
    {
        [SerializeField] private SystemsArgs _systemsArgs;
        [SerializeField] private LevelManager _levelManager;

        protected override void Awake() {
            base.Awake();
            SceneManager.OpenSceneWithArgs<SystemsController, SystemsArgs>(_systemsArgs, true);
        }

        private void Start() {
            StartCoroutine(_levelManager.UnlockDefaultGlasses());
        }
    }
}
