using Acelab.Core;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    [CreateAssetMenu(menuName = "Glasses/Portal")]
    public class SO_PortalGlasses : SO_GlassesBase {
        private ProjectionManager _projection;
        private Transform _green;
        private bool _projected;
        private Transform _player;
        private LayerMask _greenOrbs;

        public override void Equip() {
            _projected = false;
        }

        public override void Initialize(GameObject player) {
            _player = player.transform;
            _green = LevelManager.Instance.GreenOrbs;

            _projection = player.GetComponent<ProjectionManager>();
            _projection.OnProjectionToggled += ProjectionToggled;
        }

        private void ProjectionToggled(bool toggled) {
            _projected = toggled;
            if(toggled) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                foreach(Transform t in _green) {
                    t.gameObject.SetActive(true);
                }
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                foreach (Transform t in _green) {
                    t.gameObject.SetActive(false);
                }
            }
        }

        public override void Unequip() {
            foreach (Transform t in _green) {
                t.gameObject.SetActive(false);
            }
        }

        public override void Update() {
            if (!_projected) return;
            Vector2 mousePos = InputManager.Instance.GetMousePos();
            Vector3 pos = Acer.ScreenToWorldPoint(mousePos);

            Ray ray = Acer.Camera.ScreenPointToRay(pos);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 1000f, _greenOrbs, QueryTriggerInteraction.Collide)) return;

            _player.position = hit.transform.position;
        }
    }
}
