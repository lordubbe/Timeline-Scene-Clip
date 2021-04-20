using UnityEngine.Timeline;
using UnityEditor.Timeline;

namespace JRGDG.TimelineTools
{
    [CustomTimelineEditor(typeof(SceneClip))]
    public class SceneClipDrawer : ClipEditor
    {
        public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region)
        {
            //Make sure to display the currently picked scene on the clip
            clip.displayName = SceneNameUtils.GetSceneNameWithoutRelativePath((clip.asset as SceneClip).SceneName);
        }
    }
}