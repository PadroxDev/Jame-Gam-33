using Acelab.Modules.SceneManagment;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    [SceneController("Level 1")]
    public class Level1Controller : SceneController<Level1Controller, Level1Args>
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
