using System;
using AgentLogic.AgentBehaviorSuppliers;
using UnityEngine;
using Utils.Observables;

namespace AgentLogic
{
    public class BlobBrain : MonoBehaviour
    {
        //public float happiness = 0;
        [SerializeField] private AgentBehaviorSupplier<BlobBrain> behaviorSupplier;
        public ObservableFloatRegistry emotions;
        public ObservableFloatRegistry personalityTraits;
        private IAgentBehavior _agentBehavior;
        public readonly Blackboard Blackboard = new();

        private IAgentBehavior AgentBehavior => _agentBehavior ??= behaviorSupplier.GetAgentBehavior(this);
        

        void Start()
        {
            //Blackboard.Set("wanderTime", 2f);
            Blackboard.Set("waitTime", 2f);
            Blackboard.Set("wanderTarget", Vector3.zero);
            Blackboard.Set("wanderSpeed", 1f);
        }


        // public float openness = 0;
        // public float conscientiousness = 0;
        // public float extraversion = 0;
        // public float agreeableness = 0;
        // public float neuroticism = 0;

        // Update is called once per frame
        void FixedUpdate()
        {
            AgentBehavior.Tick();
        }
    }
}
