using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class NavMeshControl : MonoBehaviour
{
    
    [SerializeField] private List<Transform> _PatrolPoints;
    //[SerializeField] private List<int> _DanceIndex;
    [SerializeField] private string _DanceAreaName;
    private int _DanceAreaIndex;
    private int _currentPoint = -1;

    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _isWaiting;

    private static HashSet<int> _occupedPoint = new HashSet<int>();
    

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _DanceAreaIndex = NavMesh.GetAreaFromName(_DanceAreaName);
        MoveNextPoint();
    }

    private void Update()
    {
        if (!_isWaiting && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
           if(IsAgentInDance())
           {
            StartCoroutine(StartDance());
           }
           else
           {
            MoveNextPoint();
           }
        }
        _animator.SetFloat("Velocity",_agent.velocity.magnitude);
    }

    private void MoveNextPoint()
    {
        if(_currentPoint >=0)
           _occupedPoint.Remove(_currentPoint);

        var _availabe = Enumerable.Range(0, _PatrolPoints.Count).Where(i => !_occupedPoint.Contains(i)).ToList();

        if(_availabe.Count == 0)
           _availabe = Enumerable.Range(0, _PatrolPoints.Count).Where(i => i != _currentPoint).ToList();


        _currentPoint = _availabe[Random.Range(0, _availabe.Count)];
        _occupedPoint.Add(_currentPoint);

        _agent.isStopped= false;
        _agent.SetDestination(_PatrolPoints[_currentPoint].position);
    }

    private IEnumerator StartDance()
   {
    _isWaiting = true;
    _agent.isStopped = true;

    float _timeDance = Random.Range(10,55);
    int _randomDance = Random.Range(0,2);
    _animator.SetTrigger("Dance");
    _animator.SetInteger("RandomDance", _randomDance);

    yield return new WaitForSeconds(_timeDance);

    _animator.ResetTrigger("Dance");
    _isWaiting = false;
    _agent.isStopped = false;
    MoveNextPoint();
   }

   private bool IsAgentInDance()
   {
    NavMeshHit _hit;

    if(NavMesh.SamplePosition(transform.position, out _hit, 1f, NavMesh.AllAreas))
    {
        return _hit.mask == (1 << _DanceAreaIndex);
    }
    return false;
   }
}
       

