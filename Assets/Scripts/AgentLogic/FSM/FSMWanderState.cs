using UnityEngine;

namespace AgentLogic.FSM
{
    public class FSMWanderState : IState
    {
        private readonly BlobBrain _brain;

        public FSMWanderState(BlobBrain brain)
        {
            _brain = brain;
        }
        
        public void Tick()
        {
            float happiness = _brain.emotions.GetBetween01("happiness");
            float speed = _brain.Blackboard.Get<float>("wanderSpeed") * Mathf.Lerp(0.5f, 1.5f, happiness);
            
            Vector3 target = _brain.Blackboard.Get<Vector3>("wanderTarget");
            
            _brain.transform.position = Vector3.MoveTowards(_brain.transform.position,
                target,
                speed * _brain.DeltaTime()
            );
        }

        public void OnEnter()
        {
            float radius = Mathf.Lerp(1f, 3f, _brain.personalityTraits.GetBetween01("openness"));
            
            Vector2 dir = Random.insideUnitCircle.normalized;
            Vector3 target = _brain.transform.position + new Vector3(dir.x, dir.y, 0f) * radius;

            _brain.Blackboard.Set("wanderTarget", target);
        }

        public void OnExit()
        {
            
        }
    }
}