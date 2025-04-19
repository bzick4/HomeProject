using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private float _switchInterval = 3f; 
    [SerializeField] private GameObject[] _ObjectsToControl;

    private int _lastActiveIndex = -1;

    private void Start()
    {
        StartCoroutine(ObjectSwitchRoutine());
    }

    private IEnumerator ObjectSwitchRoutine()
    {
        while (true)
        {
            SetAllObjectsActive(false);
            
            ActivateRandomObject();
            
            yield return new WaitForSeconds(_switchInterval);
        }
    }

    private void ActivateRandomObject()
    {
        int randomIndex;
        
        do
        {
            randomIndex = Random.Range(0, _ObjectsToControl.Length);
        } 
        while (randomIndex == _lastActiveIndex && _ObjectsToControl.Length > 1);
        
        _lastActiveIndex = randomIndex;
        _ObjectsToControl[randomIndex].SetActive(true);
    }

    private void SetAllObjectsActive(bool state)
    {
        foreach (GameObject obj in _ObjectsToControl)
        {
            if (obj != null)
            {
                obj.SetActive(state);
            }
        }
    }
}

