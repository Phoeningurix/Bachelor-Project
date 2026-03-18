using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class SceneNameDisplay : MonoBehaviour
    {
        private TMP_Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();

            string currentSceneName = SceneManager.GetActiveScene().name;

            _textComponent.text = currentSceneName;
        }
    }
}
