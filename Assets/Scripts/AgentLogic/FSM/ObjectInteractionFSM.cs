using System;
using UnityEngine.InputSystem;
using Utils;
using Random = UnityEngine.Random;


namespace AgentLogic.FSM
{
    public class ObjectInteractionFSM
    {
        private StateMachine _stateMachine;
        
        public ObjectInteractionFSM(BlobBrain brain)
        {
            _stateMachine = new StateMachine();
            
            var wander = new FSMWanderState(brain);
            var idle = new FSMIdleState(brain);
            var getObject = new FSMGatherObjectState();
            
            // Normale States
            At(idle, wander, CanWander() & IsNotIdle());
            At(idle, idle, IsNotIdle());
            At(wander, wander, CanWander() & ReachedTarget());
            At(wander, idle, ReachedTarget());
            
            // Unterbrechende States
            Ata(idle, SpacePressed());
            
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

            BoolPredicate CanWander() => new(() =>
                Random.value <= brain.emotions.GetBetween01("happiness"));

            BoolPredicate SpacePressed() => new(() => Keyboard.current.spaceKey.wasPressedThisFrame);
            
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}