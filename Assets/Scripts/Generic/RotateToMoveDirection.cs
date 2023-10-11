using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMoveDirection : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100;
    [SerializeField] private float maxRadiansDelta = 1;
    private Vector3 lastPosition, currentDirection, targetDirection;
    

    void Update()
    {
            currentDirection = transform.position - lastPosition;
            targetDirection = 
                Vector3.RotateTowards(transform.forward, currentDirection, maxRadiansDelta, Time.deltaTime);
            transform.rotation = 
                Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), 
                Time.deltaTime * _rotationSpeed);
            lastPosition = transform.position;
    }
   
}
