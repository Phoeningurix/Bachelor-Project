using System;
using System.Collections.Generic;
using AgentLogic.AgentBehaviorSuppliers;
using Interactions;
using Interactions.BlobInteractions;
using Renderers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Utils.Observables;

namespace AgentLogic
{
    [RequireComponent(typeof(InteractionLocator))]
    public class BlobBrain : MonoBehaviour
    {
        [SerializeField] private AgentBehaviorSupplier<BlobBrain> behaviorSupplier;
        public ObservableFloatRegistry emotions;
        public ObservableFloatRegistry personalityTraits;
        private IAgentBehavior _agentBehavior;
        public readonly Blackboard Blackboard = new();
        [DoNotSerialize] public NavMeshAgent NavMeshAgent;
        public readonly List<BlobInteraction> InteractionRequests = new List<BlobInteraction>(); 
        public SpeechBubbleRenderer speechBubble;
        
        [DoNotSerialize] public InteractionLocator interactionLocator;

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
            
            interactionLocator = GetComponent<InteractionLocator>();
            //speechBubble = GetComponent<SpeechBubbleRenderer>();
            
            Blackboard.Set("waitTime", 2f);
            Blackboard.Set("wanderTarget", NavMeshAgent.destination);
            Blackboard.Set("wanderSpeed", 1f);
            Blackboard.Set("targetObject", NavMeshAgent.destination);
            // float temp = Mathf.InverseLerp(-1f, 1f, personalityTraits["openness"].Value);
            Blackboard.Set("objectVisibilityRadius", Mathf.Lerp(1f, 7f, personalityTraits.GetBetween01("openness")));
            Blackboard.Set("objectInteractionRadius", 0.5f);
            Blackboard.Set("hasObject", false);
            Blackboard.Set("apples", 0);
            Blackboard.Set("flowers", 0);
            Blackboard.Set("agentInteractionRadius", 5f);
            Blackboard.Set("agentInteractionWaitTime", 10f);
            Blackboard.Set("agentResponseWaitTime", 1f);
            Blackboard.Set("agentInteractionCooldown", 4f);
            Blackboard.Set("lastAgentInteractionCompleted", -100f);
            Blackboard.Set("LastRequestReceivedTimeStamp", -100f);
            Blackboard.Set("agentInteractionInvoked", -100f);
            Blackboard.Set("receivedResponse", false);

        }

        public float DeltaTime() => Time.deltaTime;
        
        void Update()
        {
            AgentBehavior.Tick();
            //if(Keyboard.current.fKey.wasPressedThisFrame) Temp();
        }

        private void Temp()
        {
            List<Interactable> interactables = interactionLocator.FindInteractablesInRange(5);
            if (interactables.Count > 0)
            {
                interactables[0].Invoke(this, () =>
                    {
                        ModifyEmotion("happiness", 0.1f);
                        Debug.Log("Interaction success");
                    }, () =>
                    {
                        ModifyEmotion("happiness", -0.1f);
                        Debug.Log("Interaction failure");
                    });
            }
            

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.orange;
            Gizmos.DrawWireSphere(transform.position, Blackboard.Get<float>("objectVisibilityRadius"));
        }
        
        
        public void ModifyEmotion(string emotion, float value)
        {
            float factor = Mathf.Lerp(0.1f, 2f, personalityTraits.GetBetween01("neuroticism"));

            float scaledDelta = value * factor;

            float newValue = emotions[emotion].Value + scaledDelta;
            emotions[emotion].Value = Mathf.Clamp(newValue, -1f, 1f);
        }
        
        public void RequestInteraction(BlobInteraction interaction)
        {
            InteractionRequests.Add(interaction);
            Blackboard.Set("LastRequestReceivedTimeStamp", Time.time);
        }

    }
}
