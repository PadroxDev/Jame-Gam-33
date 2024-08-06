using Acelab.Core;
using Acelab.Modules.SceneManagment;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mini_Jame_Gam_3
{
    [SceneController("Systems")]
    public class SystemsController : SceneController<SystemsController, SystemsArgs>
    {
        [SerializeField] private Transform _player;

        protected override void Awake() {
            base.Awake();
            StartCoroutine(Delay());
        }

        private IEnumerator Delay() {
            yield return Acer.GetWait(0.5f);
            _player.position = Args.SpawnPoint.position;
        }
    }
}
