using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace JRGDG.TimelineTools
{
    [TrackColor(1f, 0f, 1f)]
    [TrackClipType(typeof(SceneClip))]
    public class SceneClipTrack : TrackAsset
    {
        [SceneNamePicker]
        public string DefaultScene; //This should be the scene in which your main timeline director resides

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var trackMixer = ScriptPlayable<SceneClipTrackMixer>.Create(graph, inputCount);
            trackMixer.GetBehaviour().DefaultScene = DefaultScene;
            return trackMixer;
        }
    }
}