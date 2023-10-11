using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    // Update is called once per frame
    void Update()
    {
       transform.LookAt(_mainCamera.transform); 
    }
}
