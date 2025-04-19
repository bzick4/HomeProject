using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class SecurityPatrol : MonoBehaviour
{
    [SerializeField] private string _PatrolAreaName;

    private NavMeshAgent _agent;
    private Animator _animator;
    private int _areaIndex;
    private int _areaMask;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _areaIndex = NavMesh.GetAreaFromName(_PatrolAreaName);
        if (_areaIndex < 0)
            Debug.LogError($"Area \"{_PatrolAreaName}\" не найдена!");
        _areaMask = 1 << _areaIndex;

        PickNextTarget();
    }

    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            PickNextTarget();

            _animator.SetFloat("Velocity",_agent.velocity.magnitude);
    }

    private void PickNextTarget()
    {
    
        var allOthers = GameObject.FindGameObjectsWithTag("Visitor").Select(go => go.transform);

        
        var candidates = allOthers
            .Where(t => IsInPatrolArea(t.position))
            .ToList();

        if (candidates.Count == 0)
        {
            Debug.LogWarning($"Нет целей в зоне  {_PatrolAreaName}");
            return;
        }

        Transform next = candidates[Random.Range(0, candidates.Count)];
        _agent.SetDestination(next.position);
    }

    bool IsInPatrolArea(Vector3 worldPos)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(worldPos, out hit, 1f, NavMesh.AllAreas))
        {
            return (_areaMask & hit.mask) != 0;
        }
        return false;
    }
}