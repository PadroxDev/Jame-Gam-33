using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class RedObjects : MonoBehaviour
    {
        public void ShowRed() {
            Toggle(true);
        }

        public void HideRed() {
            Toggle(false);
        }

        private void Toggle(bool value) {
            foreach (Transform t in transform) {
                t.gameObject.SetActive(value);
            }
        }
    }
}