using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public BlobSelectionManager BlobSelectionManager { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                Debug.LogWarning("GameManager.Awake executed a second time!");
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Get other managers 
            BlobSelectionManager = GetComponentInChildren<BlobSelectionManager>();
        }
    }
}
