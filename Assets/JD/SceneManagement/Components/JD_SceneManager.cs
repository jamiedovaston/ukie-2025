using UnityEngine;
using JD.Utility.General;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

namespace JD.SceneManagement
{
    public class JD_SceneManager : MonoSingleton<JD_SceneManager>
    {
        public ILoadingScreenable loading { get { return InterfaceUtility.FindObjectWithInterface<ILoadingScreenable>(true); } }

        [SerializeField] private List<Scene> allScenes = new List<Scene>();

        /// <summary>
        /// Custom Function for loading scenes.
        /// </summary>
        /// <param name="_id"> Scene ID</param>
        public void LoadScene(string _id)
        {
            StartCoroutine(C_OpenSceneWithLoadingAsync(GetSceneFromID(_id)));
        }

        private IEnumerator C_OpenSceneWithLoadingAsync(Scene _scene)
        {
            if (loading == null) Debug.LogWarning($"No loading screen found in scene!", this);
            loading?.SetActive(true);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene.index);

            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                loading?.SetProgress(progress);
                loading?.SetTitle($"{_scene.name} | {(progress * 100f).ToString("F0")}%");

                yield return null;
            }

            loading?.SetActive(false);
        }

        public Scene GetSceneFromID(string _id)
        {
            Scene scene = allScenes.Find(s => s.ID == _id);
            if (scene != null) return scene;

            Debug.LogError($"Scene was not found from ID : ({_id})!", this);
            return null;
        }
    }

    [System.Serializable]
    public class Scene
    {
        [SerializeField] private string iD;
        [SerializeField] private string displayName;
        [SerializeField] private int sceneIndex;

        public string ID { get { return iD; } }
        public string name { get { return displayName; } }
        public int index { get { return sceneIndex; } }
    }
}