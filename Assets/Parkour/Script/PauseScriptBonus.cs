using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PauseScriptBonus : MonoBehaviour
{

    [SerializeField] private GameObject _Menu;
    private bool isPauseMenu=false;

    private Animator _animator;
    private PerimeterChecker _perimeterChecker;
    private ParkourControllerScript _parkourControllerScript;
    private CharacterController _characterController;
    private Controller _controller;


    void Start()
    {
        _animator =GetComponent<Animator>();
        _characterController=GetComponent<CharacterController>();
        _controller =GetComponent<Controller>();
        _parkourControllerScript = GetComponent<ParkourControllerScript>();
        _perimeterChecker = GetComponent<PerimeterChecker>();
    }

    private void Update()
    {
        PressPause();
    }

    private void PressPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPauseMenu)
            {
                isPauseMenu = true;
                _Menu.SetActive(true);
                PauseOn();
            }
            else
            {
                isPauseMenu = false;
                _Menu.SetActive(false);
                PauseOff();
            }
        }
    }

    private void PauseOff()
    {
         _animator.enabled =true;
        _characterController.enabled = true;
        _controller.enabled = true;
        _parkourControllerScript.enabled = true;
        _perimeterChecker.enabled = true;
    }

    private void PauseOn()
    {
        _animator.enabled =false;
        _characterController.enabled = false;
        _controller.enabled = false;
        _parkourControllerScript.enabled = false;
        _perimeterChecker.enabled = false;
    }
}
