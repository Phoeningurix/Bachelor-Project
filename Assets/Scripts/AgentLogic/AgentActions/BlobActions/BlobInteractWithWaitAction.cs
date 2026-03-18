using System;
using Interactions;
using UnityEngine;

namespace AgentLogic.AgentActions.BlobActions
{
    public class BlobInteractWithWaitAction : AgentAction
    {
        private readonly BlobBrain _agent;
        private Interactable _targetObject;

        private bool _hasInvoked = false;
        private bool _interactionFinished = false;

        private readonly Action _onSuccess;
        private readonly Action _onFailure;

        public BlobInteractWithWaitAction(BlobBrain agent, Action onSuccess, Action onFailure)
        {
            _agent = agent;
            _onSuccess = onSuccess;
            _onFailure = onFailure;
        }

        public override bool Tick()
        {
            if (!_hasInvoked)
            {
                _interactionFinished = false;
                _targetObject = _agent.Blackboard.Get<Interactable>("targetObject");
                _targetObject.Invoke(_agent, () =>
                {
                    _onSuccess?.Invoke();
                    _interactionFinished = true;
                }, () =>
                {
                    _onFailure?.Invoke();
                    _interactionFinished = true;
                });
                _hasInvoked = true;
            }

            if (_interactionFinished)
            {
                _interactionFinished = false;
                _hasInvoked = false;
                return true;
            }
            
            return false;
        }
    }
}