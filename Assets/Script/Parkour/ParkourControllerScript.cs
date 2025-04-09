using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourControllerScript : MonoBehaviour
{
    private PerimeterChecker _perimeterChecker;
    private Animator _animator;
    private CharacterController _charactrController;
    private Controller _controllerScript;
    private bool _isPlayerInAction;
    private int _randomJump;

    [Header("Parkour Action Area")]
    [SerializeField] private List<NewParkourAction> _NewParkourActions;


    private void Awake()
    {
        _perimeterChecker = GetComponent<PerimeterChecker>();
        _animator = GetComponent<Animator>();
        _charactrController = GetComponent<CharacterController>();
        _controllerScript = GetComponent<Controller>();
    }

    private void Update()
    {
        HitDataAction();
        Roll();
        Slide();
        JumpRandom();
       
    }

    private void HitDataAction()
    {
        var _hitData = _perimeterChecker.CheckObstacle();
        
       if(Input.GetKeyDown(KeyCode.Q) && !_isPlayerInAction)
       {
    
        if(_hitData.isHitFound)
        {
            Debug.Log(_hitData.HitInfo.transform.name);
            
            foreach (var action in _NewParkourActions)
            {
                if(action.IsCheckAvailable(_hitData, transform))
                {
                    StartCoroutine(PerformParkourAction(action));
                    break;
                }
            }
        }
       }
    }

    private IEnumerator PerformParkourAction(NewParkourAction action)
    {
        _isPlayerInAction = true;
        _controllerScript.SetControl(false);

        _animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;

        var animatorState  = _animator.GetNextAnimatorStateInfo(0);
        if(!animatorState.IsName(action.AnimationName)) Debug.Log("Anim Name Error");

        yield return new WaitForSeconds(animatorState.length);

        float timeCounter = 1f;

        while(timeCounter <= animatorState.length)
        {
            timeCounter += Time.deltaTime;

            if(action.IsLookAtObstacle)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation, _controllerScript.RotSpeed * Time.deltaTime);
            }

            if(action.IsAllowTargetMatching)
            {
                CompareTarget(action);
            }

            if(_animator.IsInTransition(0) && timeCounter >0.5f) break;

            yield return null;
        }

        yield return new WaitForSeconds(action.ParkouaActionDelay);

        _controllerScript.SetControl(true);
        _isPlayerInAction = false;
    }

    private void JumpRandom()
    {
        if(Input.GetKeyDown(KeyCode.F) )
        {
        _randomJump = Random.Range(0, 4);
        _animator.SetInteger("RandomJumpINT", _randomJump);
        _animator.SetTrigger("RandomJumpTR");
        Debug.Log(_randomJump);
        StartCoroutine(ControllOn());
        }
    }

    private void Roll()
    {
        if(Input.GetKeyDown(KeyCode.C) && _controllerScript.MovementAmount > 0f)
        {
            _animator.SetTrigger("Roll");
            StartCoroutine(ControllOn());
        }
    }

    private void Slide()
    {
        if(Input.GetKeyDown(KeyCode.X) && _controllerScript.MovementAmount > 0f)
        {
            _animator.SetTrigger("Slide");
            StartCoroutine(ControllOn());
        }
    }


    private IEnumerator ControllOn()
    {
        _charactrController.enabled = false;
        yield return new WaitForSeconds(0.4f);
        _charactrController.enabled = true;
    }


    private void CompareTarget(NewParkourAction action)
    {
        _animator.MatchTarget(action.ComparePosition, transform.rotation, action.AvatarTarget,
        new MatchTargetWeightMask(action.ComparePositionWeight, 0),action.CompareStartTime, action.CompareEndTime);
    }
}
