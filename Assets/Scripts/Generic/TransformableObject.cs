using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformableObject : MonoBehaviour
{

    [SerializeField] private float _scaleFactor, _rotationSpeed;
    
    void Rotate(Vector3 direction)
    {
        transform.Rotate(direction);
    }

    void Scale(float direction)
    {
        _scaleFactor *= Mathf.Sign(direction);
        transform.localScale += new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);
    }

}
