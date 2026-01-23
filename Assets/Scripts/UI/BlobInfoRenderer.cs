using AgentLogic;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class BlobInfoRenderer : MonoBehaviour
    {
        private VisualElement _root;
        private Label _agentNameLabel;
        private Slider _happinessSlider;
        private Slider _opennessSlider;


        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.style.display = DisplayStyle.None;
            
            _agentNameLabel = _root.Q<Label>("agentName");
            _happinessSlider = _root.Q<Slider>("happiness");
            _opennessSlider = _root.Q<Slider>("openness");
            
        }
        
        private void OnEnable()
        {
            Debug.Log("Game Manager: " + GameManager.Instance);
            Debug.Log("Game Manager: " + GameManager.Instance.BlobSelectionManager);
            GameManager.Instance.BlobSelectionManager.OnSelectionChanged += OnBlobChanged;
        }

        private void OnDisable()
        {
            GameManager.Instance.BlobSelectionManager.OnSelectionChanged -= OnBlobChanged;
        }

        private void OnBlobChanged(BlobBrain brain)
        {
            Debug.Log("Blob Changed");
            if (brain == null)
            {
                HideUI();
            }
            else
            {
                SetUIValues(brain);
                ShowUI();
            }
        }

        private void ShowUI()
        {
            _root.style.display = DisplayStyle.Flex;
        }

        private void HideUI()
        {
            _root.style.display = DisplayStyle.None;
        }

        private void SetUIValues(BlobBrain brain)
        {
            _agentNameLabel.text = brain.name;
            _happinessSlider.value = brain.emotions["happiness"].Value;
            _opennessSlider.value = brain.personalityTraits["openness"].Value;
        }
    }
}