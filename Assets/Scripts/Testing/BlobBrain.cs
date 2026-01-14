using System;
using AgentLogic;
using AgentLogic.Testing.BehaviorTree;
using UnityEngine;
using Utils.Observables;

namespace Testing
{
    public class BlobBrain : MonoBehaviour
    {
        //public float happiness = 0;
        public ObservableFloatRegistry emotions;
        public AgentBehavior<BlobBrain> behavior;
        [NonSerialized]private BlobBehaviorTree _behaviorTree;
        [NonSerialized]public bool HasWanderTarget = false;
        [NonSerialized]public bool IsIdle = false;

        public Vector3 WanderTarget;

        public ObservableFloatRegistry personalityTraits;

        // public float openness = 0;
        // public float conscientiousness = 0;
        // public float extraversion = 0;
        // public float agreeableness = 0;
        // public float neuroticism = 0;

        private void Awake()
        {
            _behaviorTree = new BlobBehaviorTree(this);
        }

        private AgentAction<BlobBrain> _currentAction;
        
        // Update is called once per frame
        void FixedUpdate()
        {
            //BehaviorLoop();
            // Hier die Root vom Behavior Tree anticken
            _behaviorTree.root.Tick();
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
