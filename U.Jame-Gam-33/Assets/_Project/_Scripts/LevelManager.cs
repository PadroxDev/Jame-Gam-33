using Acelab.Core.Utility;
using UnityEngine;

namespace Mini_Jame_Gam_3
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private RedObjects _redObjs;

        public RedObjects RedObjs { get { return _redObjs; } }
    }
}
