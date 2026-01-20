using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AgentLogic.FSM
{
    public class SimpleBlobFSM : IAgentBehavior
    {
        private StateMachine _stateMachine;

        public SimpleBlobFSM(BlobBrain brain)
        {
            _stateMachine = new StateMachine();
            
            var setWanderTarget = new FSMSetWanderTarget(brain);
            var wander = new FSMWanderState(brain);
            var idle = new FSMIdleState(brain);
            var canWander = new FSMWanderConditionState(brain);
            
            At(idle, setWanderTarget, WanderCondition());
            At(idle, idle, IsNotIdle());
            At(setWanderTarget, wander, HasWanderTarget());
            At(wander, setWanderTarget, ContinueWanderCondition());
            At(wander, idle, ReachedTarget());
            
            void At(IState from, IState to, Func<bool> condition) => 
                _stateMachine.AddTransition(from, to, condition);
            
            // States, die andere unterbrechen, können mit _stateMachine.AddAnyState(...) hinzugefügt werden
            
            _stateMachine.SetState(setWanderTarget);

            // Conditions
            Func<bool> IsNotIdle() => () =>
                brain.Blackboard.Get<float>("idleTimeSinceStart") > brain.Blackboard.Get<float>("waitTime");
            Func<bool> WanderCondition() => 
                () => Random.value <= Mathf.Clamp01(brain.emotions["happiness"].Value / 2f + 0.5f) 
                      && brain.Blackboard.Get<float>("idleTimeSinceStart") > brain.Blackboard.Get<float>("waitTime");
            Func<bool> ContinueWanderCondition() => 
                () => Random.value <= Mathf.Clamp01(brain.emotions["happiness"].Value / 2f + 0.5f) 
                      && Vector3.Distance(brain.Blackboard.Get<Vector3>("wanderTarget"), brain.transform.position) < 0.01f;
            Func<bool> HasWanderTarget() => () => brain.Blackboard.Get<Vector3>("wanderTarget") != Vector3.zero;
            Func<bool> ReachedTarget() => () => 
                Vector3.Distance(brain.Blackboard.Get<Vector3>("wanderTarget"), brain.transform.position) < 0.01f;
        }

        public void Tick()
        {
            _stateMachine.Tick();
        }
    }
}