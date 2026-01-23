using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class BlobInfoRenderer : MonoBehaviour
    {
        private BlobSelectionManager _currentAgent;
        
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            
            Label agentName = root.Q<Label>("agentName");
            Slider happiness = root.Q<Slider>("happiness");
            Slider openness = root.Q<Slider>("openness");
            Label label = root.Q<Label>("test");
        }
    }
}