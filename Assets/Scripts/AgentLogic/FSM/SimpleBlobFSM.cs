using System;
using AgentLogic.FSM.FSMStates;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Random = UnityEngine.Random;

namespace AgentLogic.FSM
{
    public class SimpleBlobFSM : IAgentBehavior
    {
        private StateMachine _stateMachine;

        public SimpleBlobFSM(BlobBrain brain)
        {
            _stateMachine = new StateMachine();
            
            var wander = new FSMWanderState(brain);
            var reachedTarget = new FSMReachedTargetState(); // transition state
            var idle = new FSMIdleState(brain);
            var finishedIdle = new FSMFinishedIdleState();
            var respond = new FSMRespondToInteractionRequestState(brain);
            var responseFinished = new FSMFinishedResponseState(brain); // transition state
            var sendInteraction = new FSMSendInteractionState(brain);
            var onInteractionIgnored = new FSMOnInteractionIgnoredState(brain);
            var interactionFinished = new FSMSendInteractionFinishedState(brain);


            
            // Normale States
            At(idle, finishedIdle, IsNotIdle());
            At(finishedIdle, sendInteraction, IsNearAgents() & WantsToInteract());
            At(finishedIdle, wander, CanWander());
            At(finishedIdle, idle, Always());
            
            
            
            At(wander, reachedTarget, ReachedTarget());
            
            At(reachedTarget, wander, CanWander());
            At(reachedTarget, sendInteraction, IsNearAgents() & WantsToInteract());
            At(reachedTarget, idle, Always());
            
            At(respond, responseFinished, DelayCondition(
                () => brain.Blackboard.Get<float>("LastRequestReceivedTimeStamp"), 
                brain.Blackboard.Get<float>("agentResponseWaitTime")
                )
            );
            
            At(responseFinished, wander, CanWander());
            At(responseFinished, idle, Always());
            
            At(sendInteraction, onInteractionIgnored, DelayCondition(() => brain.Blackboard.Get<float>("agentInteractionInvoked"), brain.Blackboard.Get<float>("agentInteractionWaitTime")));
            At(onInteractionIgnored, interactionFinished, Always());
            At(sendInteraction, interactionFinished, HasReceivedResponse());
            
            At(interactionFinished, wander, CanWander());
            At(interactionFinished, idle, Always());
            
            // Unterbrechende States
            Ata(idle, SpacePressed());
            Ata(respond, HasRequests() & !CheckIgnoreRequest());
            
            void At(IState from, IState to, Func<bool> condition) => 
                _stateMachine.AddTransition(from, to, condition);
            
            // Add any transition
            void Ata(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
            
            _stateMachine.SetState(CanWander() ? wander : idle);
            
            // Conditions
            BoolPredicate IsNotIdle() => 
                new(() => !idle.IsIdle());

            BoolPredicate ReachedTarget() => new(() => 
                brain.NavMeshAgent.enabled && !brain.NavMeshAgent.pathPending && brain.NavMeshAgent.remainingDistance <= brain.NavMeshAgent.stoppingDistance);

            BoolPredicate CanWander() => new(() => DecisionUtils.CanWander(brain));

            BoolPredicate SpacePressed() => new(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            BoolPredicate Always() => new(() => true);

            BoolPredicate HasRequests() => new(() => brain.InteractionRequests.Count > 0);

            // CheckIgnoreRequest deletes a request if ignored
            BoolPredicate CheckIgnoreRequest() => new(() =>
            {
                if (brain.InteractionRequests.Count == 0)
                {
                    Debug.LogError("FSM tried to ignore a non-existing request");
                    return true;
                }

                if (DecisionUtils.CheckIgnore(brain))
                {
                    brain.InteractionRequests.RemoveAt(0);
                    return true;
                }
                return false;
            });

            BoolPredicate DelayCondition(Func<float> startTimestampSupplier, float delay) =>
                new(() =>
                {
                    return Time.time >= startTimestampSupplier.Invoke() + delay;
                });
            
            //BoolPredicate HasReceivedRequestThisTick() => new(() => brain.Blackboard.Get<float>("LastRequestReceivedTimeStamp") ==);
            
            BoolPredicate HasReceivedResponse() => new(() => brain.Blackboard.Get<bool>("receivedResponse"));

            BoolPredicate IsNearAgents() => new(() =>
                brain.interactionLocator.IsNearAgents(brain.Blackboard.Get<float>("agentInteractionRadius")));

            BoolPredicate WantsToInteract() => new(() => DecisionUtils.CheckSendInteraction(brain));
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}