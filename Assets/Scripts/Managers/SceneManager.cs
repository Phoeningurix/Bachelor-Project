using UnityEngine;
using UnityEngine.InputSystem; 
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; 

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        void Update()
        {
            if (Keyboard.current == null) return;
            
            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                SwitchToNextScene();
            }
            
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                SwitchToPrevScene();
            }

            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                GoToLastScene();
            }
        }
        
        private void SwitchToNextScene()
        {
            int currentIndex = UnitySceneManager.GetActiveScene().buildIndex;
            int totalScenes = UnitySceneManager.sceneCountInBuildSettings;

            int nextIndex = (currentIndex + 1) % totalScenes;
            LoadSceneByIndex(nextIndex);
        }

        private void SwitchToPrevScene()
        {
            int currentIndex = UnitySceneManager.GetActiveScene().buildIndex;
            int totalScenes = UnitySceneManager.sceneCountInBuildSettings;

            int prevIndex = (currentIndex - 1 + totalScenes) % totalScenes;
            LoadSceneByIndex(prevIndex);
        }
        
        public void GoToLastScene()
        {
            int totalScenes = UnitySceneManager.sceneCountInBuildSettings;

            if (totalScenes <= 0) 
            {
                Debug.LogWarning("No scenes found in Build Settings!");
                return;
            }

            // The last index is always (Total Count - 1)
            int lastIndex = totalScenes - 1;
        
            LoadSceneByIndex(lastIndex);
        }

        private void LoadSceneByIndex(int index)
        {
            // Get the path, then strip it down to just the name for the log
            string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(index);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            Debug.Log($"Switching to scene <<{sceneName}>> at index {index}");
            UnitySceneManager.LoadScene(index);
        }
    }
}