using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneManagementBehaviour : MonoBehaviour
    {
        public void LoadNextScene()
        {
            var current = SceneManager.GetActiveScene().buildIndex;
            var next = current + 1;

            if (next >= SceneManager.sceneCountInBuildSettings)
            {
                next = 0;
            }

            SceneManager.LoadScene(next);
        }

        public void LoadNextSceneAfterDelay(float delay)
        {
            this.ExecuteAfter(delay, LoadNextScene);
        }

        public void LoadSceneByName(string sceneName)
        {
            LoadScene(sceneName);
        }

        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}