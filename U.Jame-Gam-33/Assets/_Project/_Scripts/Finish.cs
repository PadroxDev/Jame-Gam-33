using Acelab.Modules.SceneManagment;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class Finish : MonoBehaviour
    {
        public bool GoToCredit = false;

        private void OnTriggerEnter(Collider other) {
            if (other.transform.parent.tag != "Player") return;

            if (GoToCredit)
                SceneManager.OpenSceneWithArgs<CreditsController, CreditsArgs>();
            else
                SceneManager.OpenSceneWithArgs<MainMenuController, MainMenuArgs>();
        }
    }
}
