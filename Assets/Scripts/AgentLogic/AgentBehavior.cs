using Testing;
using UnityEngine;

namespace AgentLogic
{
    public abstract class AgentBehavior<T>: ScriptableObject
    {

        public abstract AgentAction<T> GetAction(T agent);

    }
}