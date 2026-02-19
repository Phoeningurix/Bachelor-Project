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
        private Label _agentLogicLabel;
        
        // Emotions
        private Slider _happinessSlider;
        private Slider _fearSlider;
        private Slider _angerSlider;
        
        // Personality Traits
        private Slider _opennessSlider;
        private Slider _conscientiousnessSlider;
        private Slider _extraversionSlider;
        private Slider _agreeablenessSlider;
        private Slider _neuroticismSlider;

        private BlobBrain _currentBrain;


        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.style.display = DisplayStyle.None;
            
            _agentNameLabel = _root.Q<Label>("agentName");
            _agentLogicLabel = _root.Q<Label>("agentLogic");
            
            _happinessSlider = _root.Q<Slider>("happiness");
            _fearSlider = _root.Q<Slider>("fear");
            _angerSlider = _root.Q<Slider>("anger");
            
            _opennessSlider = _root.Q<Slider>("openness");
            _conscientiousnessSlider = _root.Q<Slider>("conscientiousness");
            _extraversionSlider = _root.Q<Slider>("extraversion");
            _agreeablenessSlider = _root.Q<Slider>("agreeableness");
            _neuroticismSlider = _root.Q<Slider>("neuroticism");
            
            
        }
        
        private void OnEnable()
        {
            //Debug.Log("Game Manager: " + GameManager.Instance);
            //Debug.Log("Game Manager: " + GameManager.Instance.BlobSelectionManager);
            GameManager.Instance.BlobSelectionManager.OnSelectionChanged += OnBlobChanged;
            
            _happinessSlider.RegisterValueChangedCallback(OnHappinessSliderValueChanged);
            _fearSlider.RegisterValueChangedCallback(OnFearSliderValueChanged);
            _angerSlider.RegisterValueChangedCallback(OnAngerSliderValueChanged);
            
            _opennessSlider.RegisterValueChangedCallback(OnOpennessSliderValueChanged);
            _conscientiousnessSlider.RegisterValueChangedCallback(OnConscientiousnessSliderValueChanged);
            _extraversionSlider.RegisterValueChangedCallback(OnExtraversionSliderValueChanged);
            _agreeablenessSlider.RegisterValueChangedCallback(OnAgreeablenessSliderValueChanged);
            _neuroticismSlider.RegisterValueChangedCallback(OnNeuroticismSliderValueChanged);
        }

        private void OnDisable()
        {
            GameManager.Instance.BlobSelectionManager.OnSelectionChanged -= OnBlobChanged;
            
            _happinessSlider.UnregisterValueChangedCallback(OnHappinessSliderValueChanged);
            _fearSlider.UnregisterValueChangedCallback(OnFearSliderValueChanged);
            _angerSlider.UnregisterValueChangedCallback(OnAngerSliderValueChanged);
            
            _opennessSlider.UnregisterValueChangedCallback(OnOpennessSliderValueChanged);
            _conscientiousnessSlider.UnregisterValueChangedCallback(OnConscientiousnessSliderValueChanged);
            _extraversionSlider.UnregisterValueChangedCallback(OnExtraversionSliderValueChanged);
            _agreeablenessSlider.UnregisterValueChangedCallback(OnAgreeablenessSliderValueChanged);
            _neuroticismSlider.UnregisterValueChangedCallback(OnNeuroticismSliderValueChanged);
        }

        private void OnBlobChanged(BlobBrain brain)
        {
            if (_currentBrain != null)
            {
                UnsubscribeFromAgent(_currentBrain);
            }
            
            _currentBrain = brain;
            
            //Debug.Log("Blob Changed");
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
            _agentLogicLabel.text = brain.BehaviorType;
        }

        private void SubscribeToAgent(BlobBrain brain)
        {
            // Subcribe to emotions
            brain.emotions["happiness"].OnChanged += OnHappinessChanged;
            brain.emotions["fear"].OnChanged += OnFearChanged;
            brain.emotions["anger"].OnChanged += OnAngerChanged;
            
            // Subscribe to personality traits
            brain.personalityTraits["openness"].OnChanged += OnOpennessChanged;
            brain.personalityTraits["conscientiousness"].OnChanged += OnConscientiousnessChanged;
            brain.personalityTraits["extraversion"].OnChanged += OnExtraversionChanged;
            brain.personalityTraits["agreeableness"].OnChanged += OnAgreeablenessChanged;
            brain.personalityTraits["neuroticism"].OnChanged += OnNeuroticismChanged;
            
        }

        private void UnsubscribeFromAgent(BlobBrain brain)
        {
            // Unsubcribe to emotions
            brain.emotions["happiness"].OnChanged -= OnHappinessChanged;
            brain.emotions["fear"].OnChanged -= OnFearChanged;
            brain.emotions["anger"].OnChanged -= OnAngerChanged;
            
            // Unsubscribe to personality traits
            brain.personalityTraits["openness"].OnChanged -= OnOpennessChanged;
            brain.personalityTraits["conscientiousness"].OnChanged -= OnConscientiousnessChanged;
            brain.personalityTraits["extraversion"].OnChanged -= OnExtraversionChanged;
            brain.personalityTraits["agreeableness"].OnChanged -= OnAgreeablenessChanged;
            brain.personalityTraits["neuroticism"].OnChanged -= OnNeuroticismChanged;
        }
        
        
        
        // Event Methoden
        
        // Emotions

        private void OnHappinessChanged(float happiness)
        {
            _happinessSlider.value = happiness;
        }

        private void OnFearChanged(float fear)
        {
            _fearSlider.value = fear;
        }

        private void OnAngerChanged(float anger)
        {
            _angerSlider.value = anger;
        }
        
        
        // Personality Traits

        private void OnOpennessChanged(float openness)
        {
            _opennessSlider.value = openness;
        }

        private void OnConscientiousnessChanged(float conscientiousness)
        {
            _conscientiousnessSlider.value = conscientiousness;
        }

        private void OnExtraversionChanged(float extraversion)
        {
            _extraversionSlider.value = extraversion;
        }

        private void OnAgreeablenessChanged(float agreeableness)
        {
            _agreeablenessSlider.value = agreeableness;
        }

        private void OnNeuroticismChanged(float neuroticism)
        {
            _neuroticismSlider.value = neuroticism;
        }
        
        
        // Slider Events

        // Emotions
        private void OnHappinessSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null) _currentBrain.emotions["happiness"].Value = e.newValue;
        }

        private void OnFearSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null) _currentBrain.emotions["fear"].Value = e.newValue;
        }

        private void OnAngerSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null) _currentBrain.emotions["anger"].Value = e.newValue;
        }
        
        // Personality Traits
        
        private void OnOpennessSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null)
            {
                _currentBrain.personalityTraits["openness"].Value = e.newValue;
                _currentBrain.Blackboard.Set("objectVisibilityRadius", Mathf.Lerp(1f, 7f, e.newValue));
            }
        }

        private void OnConscientiousnessSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null)
            {
                _currentBrain.personalityTraits["conscientiousness"].Value = e.newValue;
            }
        }

        private void OnExtraversionSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null)
            {
                _currentBrain.personalityTraits["extraversion"].Value = e.newValue;
                //TODO Extraversion could be used to calculate the "agentInteractionRadius"
            }
        }

        private void OnAgreeablenessSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null)
            {
                _currentBrain.personalityTraits["agreeableness"].Value = e.newValue;
            }
        }

        private void OnNeuroticismSliderValueChanged(ChangeEvent<float> e)
        {
            if (_currentBrain != null)
            {
                _currentBrain.personalityTraits["neuroticism"].Value = e.newValue;
            }
        }
    }
}