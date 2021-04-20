using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace JRGDG.TimelineTools
{
    public class SceneClipTrackMixer : PlayableBehaviour
    {
        public string DefaultScene;
        private List<string> scenesToActivate = new List<string>();
        private Dictionary<SceneClipBehaviour, string> scenesThatShouldBeLoaded = new Dictionary<SceneClipBehaviour, string>();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var defaultSceneName = SceneNameUtils.GetSceneNameWithoutRelativePath(DefaultScene);

            //Loop through all clips on the track
            for (int i = 0; i < playable.GetInputCount(); i++)
            {
                var weight = playable.GetInputWeight(i);
                var inputPlayable = (ScriptPlayable<SceneClipBehaviour>)playable.GetInput(i);
                var clip = inputPlayable.GetBehaviour();

                if (weight > 0f) //Check if the playhead is over the clip
                {
                    if (!scenesThatShouldBeLoaded.ContainsKey(clip))
                    {
                        scenesThatShouldBeLoaded.Add(clip, clip.SceneName);
                        if (clip.SetActive) scenesToActivate.Add(SceneNameUtils.GetSceneNameWithoutRelativePath(clip.SceneName));
                    }
                }
                else
                {
                    if (scenesThatShouldBeLoaded.ContainsKey(clip))
                    {
                        scenesThatShouldBeLoaded.Remove(clip);
                    }
                }
            }

            //Go through and make sure that the right scenes are open
            foreach (var sceneName in scenesThatShouldBeLoaded.Values)
            {
                var isLoaded = IsSceneLoaded(sceneName);
                if (!isLoaded) LoadOrOpenScene(sceneName);
            }


            //Then check if any scenes that shouldn't be open are open
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var loadedScene = SceneManager.GetSceneAt(i);
                if (loadedScene.name != defaultSceneName && !scenesThatShouldBeLoaded.Values.Any(sceneName => SceneNameUtils.GetSceneNameWithoutRelativePath(sceneName) == loadedScene.name))
                {
                    UnloadOrCloseScene(loadedScene);
                }
            }

            base.ProcessFrame(playable, info, playerData);
        }

        private void LoadOrOpenScene(string sceneName)
        {
            if (Application.isPlaying)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                EditorSceneManager.sceneOpened += OnEditorSceneOpened;
                EditorSceneManager.OpenScene(SceneNameUtils.GetSceneNameWithProjectPath(sceneName), OpenSceneMode.Additive);
            }
        }

        private void UnloadOrCloseScene(Scene scene)
        {
            if (Application.isPlaying)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;

                if (scene.IsValid())
                    SceneManager.UnloadSceneAsync(scene);
            }
            else
            {
                EditorSceneManager.sceneOpened -= OnEditorSceneOpened;
                EditorSceneManager.CloseScene(scene, true);
            }
        }

        private bool IsSceneLoaded(string sceneWithRelativePath)
        {
            var scene = SceneManager.GetSceneByPath(SceneNameUtils.GetSceneNameWithProjectPath(sceneWithRelativePath));
            return scene != default(Scene) && scene.isLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!scenesToActivate.Contains(scene.name)) return;

            SceneManager.SetActiveScene(scene);
            scenesToActivate.Remove(scene.name);
        }

        private void OnEditorSceneOpened(Scene scene, OpenSceneMode mode)
        {
            if (!scenesToActivate.Contains(scene.name)) return;

            EditorSceneManager.SetActiveScene(scene);
            scenesToActivate.Remove(scene.name);
        }
    }
}