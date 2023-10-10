using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformableObject : InteractiveObject
{
    [Range(0,1)][SerializeField] private float _scaleFactor = 0.1f;

    private float maxScaleValue, initialScaleValue, minScaleValue;
    
    void Start()
    {
        SetScaleValues();
    }

    void SetScaleValues()
    {
        _scaleFactor *= transform.localScale.x;
        initialScaleValue = transform.localScale.x;
        maxScaleValue = initialScaleValue * 2;
        minScaleValue = initialScaleValue/2;
    }

    public void Rotate(Vector3 direction)
    {
        if(LVLController.Instance.CurrentSelectedObject != gameObject) return;
        transform.rotation = Quaternion.AngleAxis(direction.x, Vector3.up);
    }

    public void Scale(float direction)
    {
        if(LVLController.Instance.CurrentSelectedObject != gameObject) return;
        float newScaleValue = Mathf.Sign(direction) * _scaleFactor;

        if(transform.localScale.x <= maxScaleValue && transform.localScale.x >= minScaleValue)
            transform.localScale += new Vector3(newScaleValue, newScaleValue, newScaleValue);
       
        if(transform.localScale.x > maxScaleValue)
            transform.localScale = new Vector3(maxScaleValue, maxScaleValue, maxScaleValue);
        
        if(transform.localScale.x < minScaleValue)
            transform.localScale = new Vector3(minScaleValue, minScaleValue, minScaleValue);
        
    }

}
