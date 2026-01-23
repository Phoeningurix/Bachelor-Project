using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AgentMovement : MonoBehaviour
{
    private Vector3 _target;
    NavMeshAgent _agent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }
    
    void SetTargetPosition()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            if (Camera.main != null)
            {
                Debug.Log("Mouse was clicked");
                _target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            }
    }

    void SetAgentPosition()
    {
        _agent.SetDestination(new Vector3(_target.x, _target.y, transform.position.z));
    }
    
}
