using UnityEngine;

namespace Mini_Jame_Gam_3
{
    [CreateAssetMenu(menuName = "Glasses/Red")]
    public class SO_RedGlasses : SO_GlassesBase {
        private RedObjects _redObjs;

        public override void Initialize(object player) {
            _redObjs = LevelManager.Instance.RedObjs;
        }

        public override void Equip() {
            _redObjs.ShowRed();
        }

        public override void Unequip() {
            _redObjs.HideRed();
        }
    }
}
