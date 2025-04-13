using UnityEngine.AI;
using UnityEngine;

public class NavMeshControl : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private GameObject _Target; 

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = _Target.transform.position;

    }
}
