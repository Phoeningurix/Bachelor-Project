
using AgentLogic;
using Renderers;

namespace Interactions
{
    public class AppleTreeInteractable : InteractableWithWait
    {
        private AppleRenderer[] _apples;
        private int _currentAppleIndex = 0;

        
        protected override void Awake()
        {
            base.Awake();
            _apples = GetComponentsInChildren<AppleRenderer>();
        }
        
        protected override void OnSuccess(BlobBrain brain)
        {
            var apples = brain.Blackboard.Get<int>("apples");
            brain.Blackboard.Set("apples",  ++apples);
            
            OnApplePickedUp();
        }

        private void OnApplePickedUp()
        {
            if (_currentAppleIndex < _apples.Length)
            {
                _apples[_currentAppleIndex].SetVisible(false);
                _currentAppleIndex++;
            }
        }
    }
}