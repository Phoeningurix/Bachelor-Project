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

        private BlobBrain _currentBrain;


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
            if (_currentBrain != null)
            {
                UnsubscribeFromAgent(_currentBrain);
            }
            
            _currentBrain = brain;
            
            Debug.Log("Blob Changed");
            if (brain == null)
            {
                HideUI();
            }
            else
            {
                SetStaticUIValues(brain);
                SubscribeToAgent(brain);
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

        private void SetStaticUIValues(BlobBrain brain)
        {
            _agentNameLabel.text = brain.name;
            float happiness = brain.emotions["happiness"].Value;
            _happinessSlider.value = happiness;
            float openness = brain.personalityTraits["openness"].Value;
            _opennessSlider.value = openness;
        }

        private void SubscribeToAgent(BlobBrain brain)
        {
            // Subcribe to emotions
            brain.emotions["happiness"].OnChanged += OnHappinessChanged;
            
            // Subscribe to personality traits
            brain.personalityTraits["openness"].OnChanged += OnOpennessChanged;
            
        }

        private void UnsubscribeFromAgent(BlobBrain brain)
        {
            // Unsubcribe to emotions
            brain.emotions["happiness"].OnChanged -= OnHappinessChanged;
            
            // Unsubscribe to personality traits
            brain.personalityTraits["openness"].OnChanged -= OnOpennessChanged;
        }
        
        
        
        // Event Methoden

        private void OnHappinessChanged(float happiness)
        {
            _happinessSlider.value = happiness;
        }

        private void OnOpennessChanged(float openness)
        {
            _opennessSlider.value = openness;
        }
    }
}