using System;
using UnityEngine.Playables;

namespace JRGDG.TimelineTools
{
    [Serializable]
    public class SceneClipBehaviour : PlayableBehaviour
    {
        public string SceneName;
        public bool SetActive;
    }
}
