using System.Linq;
using UnityEngine.SceneManagement;

namespace JRGDG.TimelineTools
{
    public static class SceneNameUtils
    {
        public static string GetSceneNameWithoutRelativePath(string sceneName) => sceneName.Split('/').Last();
        public static string GetSceneNameWithProjectPath(string sceneName) => "Assets/" + sceneName + ".unity";
    }
}