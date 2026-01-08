using AgentLogic;
using UnityEngine;

namespace Testing
{
    public class BlobBrain : MonoBehaviour
    {
        public float happiness = 0;
        public AgentBehavior<BlobBrain> behavior;
        
        private AgentAction<BlobBrain> _currentAction;
        
        // Update is called once per frame
        void FixedUpdate()
        {
            BehaviorLoop();
        }

        private void BehaviorLoop()
        {
            if (_currentAction != null)
            {
                if (_currentAction.Tick(this, Time.fixedDeltaTime))
                {
                    _currentAction.OnStop(this);
                    _currentAction = null;
                }
            }
            else
            {
                _currentAction = behavior.GetAction(this);
                _currentAction.OnStart(this);
            }
        }
    }
}
