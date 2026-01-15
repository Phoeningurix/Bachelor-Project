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
        private IAgentBehavior _agentBehavior;

        private IAgentBehavior AgentBehavior => _agentBehavior ??= behaviorSupplier.GetAgentBehavior(this);
        
        public bool HasWanderTarget { get; set; } // TODO - remove

        public Vector3 wanderTarget;

        public ObservableFloatRegistry personalityTraits;

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
