using System;
using AgentLogic.AgentBehaviorSuppliers;
using UnityEngine;
using Utils.Observables;

namespace AgentLogic
{
    public class BlobBrain : MonoBehaviour
    {
        [SerializeField] private AgentBehaviorSupplier<BlobBrain> behaviorSupplier;
        public ObservableFloatRegistry emotions;
        public ObservableFloatRegistry personalityTraits;
        private IAgentBehavior _agentBehavior;
        public readonly Blackboard Blackboard = new();
        

        private IAgentBehavior AgentBehavior => _agentBehavior ??= behaviorSupplier.GetAgentBehavior(this);
        

        void Start()
        {
            Blackboard.Set("waitTime", 2f);
            Blackboard.Set("wanderTarget", Vector3.zero);
            Blackboard.Set("wanderSpeed", 1f);
        }

        public float DeltaTime() => Time.deltaTime;
        
        void Update()
        {
            AgentBehavior.Tick();
        }
    }
}
