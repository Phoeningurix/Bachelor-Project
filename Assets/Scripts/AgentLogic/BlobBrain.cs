using System;
using AgentLogic.AgentBehaviorSuppliers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
        [DoNotSerialize] public NavMeshAgent NavMeshAgent;

        public event Action OnSelected;
        public event Action OnUnselected;
        
        public void InvokeSelect() => OnSelected?.Invoke();
        public void InvokeUnselect() => OnUnselected?.Invoke();
        

        private IAgentBehavior AgentBehavior => _agentBehavior ??= behaviorSupplier.GetAgentBehavior(this);
        public string BehaviorType => _agentBehavior.GetType().Name;

        void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updateUpAxis = false;
            NavMeshAgent.enabled = true;
            NavMeshAgent.SetDestination(transform.position);
            
            Blackboard.Set("waitTime", 2f);
            Blackboard.Set("wanderTarget", NavMeshAgent.destination);
            Blackboard.Set("wanderSpeed", 1f);
        }

        public float DeltaTime() => Time.deltaTime;
        
        void Update()
        {
            AgentBehavior.Tick();
        }
    }
}
