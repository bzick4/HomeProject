using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pause : MonoBehaviour

{
    [SerializeField] private GameObject _Menu;
    private bool isPauseMenu=false;



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

    void PauseOn()
    {
        Animator[] allAnimator  = FindObjectsOfType<Animator>();
        foreach (Animator anim in allAnimator)
        {
            anim.enabled = false;
        }

        NavMeshAgent[] allAgent  = FindObjectsOfType<NavMeshAgent>();
        foreach (NavMeshAgent agent in allAgent)
        {
            agent.enabled = false;
        }

        NavMeshControl[] allControls = FindObjectsOfType<NavMeshControl>();
        foreach (NavMeshControl navMeshControl in allControls)
        {
            navMeshControl.enabled = false;
        }
    }

     void PauseOff()
    {
        Animator[] allAnimator  = FindObjectsOfType<Animator>();
        foreach (Animator anim in allAnimator)
        {
            anim.enabled = true;
        }

        NavMeshAgent[] allAgent  = FindObjectsOfType<NavMeshAgent>();
        foreach (NavMeshAgent agent in allAgent)
        {
            agent.enabled = true;
        }

        NavMeshControl[] allControls = FindObjectsOfType<NavMeshControl>();
        foreach (NavMeshControl navMeshControl in allControls)
        {
            navMeshControl.enabled = true;
        }
    }






}

