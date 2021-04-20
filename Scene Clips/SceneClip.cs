using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JRGDG.TimelineTools
{
    [Serializable]
    public class SceneClip : PlayableAsset, ITimelineClipAsset
    {
        [SceneNamePicker]
        public string SceneName;
        public bool SetActive = true;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SceneClipBehaviour>.Create(graph);
            SceneClipBehaviour behaviour = playable.GetBehaviour();

            behaviour.SceneName = SceneName;
            behaviour.SetActive = SetActive;

            return playable;
        }
    }
}