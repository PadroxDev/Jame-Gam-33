using Acelab.Core;
using Acelab.Core.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class LevelManager : Singleton<LevelManager>
    {
        [BoxGroup("Level"), SerializeField] private SO_GlassesBase[] _defaultGlasses;
        [BoxGroup("Level"), SerializeField] private RedObjects _redObjs;
        [BoxGroup("Level"), SerializeField] private Transform _greenOrbs;
        private GlassesWheel _glassesWheel;

        public RedObjects RedObjs { get { return _redObjs; } }
        public Transform GreenOrbs { get { return _greenOrbs; } }

        public IEnumerator UnlockDefaultGlasses() {
            yield return Acer.GetWait(0.2f);
            _glassesWheel = GlassesWheel.Instance;

            foreach (SO_GlassesBase glasses in _defaultGlasses) {
                _glassesWheel.UnlockGlasses(glasses);
            }
        }
    }
}
