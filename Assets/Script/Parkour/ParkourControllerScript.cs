using System.Collections;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
    private PerimeterChecker _perimeterChecker;
    private Animator _animator;
    private bool _isPlayerInAction;


    private void Awake()
    {
        _perimeterChecker = GetComponent<PerimeterChecker>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Q) && !_isPlayerInAction)
       {
       var _hitData = _perimeterChecker.CheckObstacle();

        if(_hitData.isHitFound)
        {
            StartCoroutine(PerformParkourAction());
        }
       }
    }

    private IEnumerator PerformParkourAction()
    {
        _isPlayerInAction = true;

        _animator.CrossFade("JumpUpObstacle", 0.2f);
        yield return null;

        var animatorState  = _animator.GetNextAnimatorStateInfo(0);

        yield return new WaitForSeconds(animatorState.length);

        _isPlayerInAction = false;
    }
}
