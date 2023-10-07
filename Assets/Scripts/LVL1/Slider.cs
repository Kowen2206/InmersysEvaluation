using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slider : MonoBehaviour
{
    float maxValue = 100, currentValue;
    [SerializeField] GameObject _bar, _handle;
    [SerializeField] LVLController _lVLController;
    BoxCollider barCollider;
    private bool isSelected;
    public bool IsSelected { set => isSelected = value; get => isSelected;}

    void Start()
    {
        barCollider = _bar.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    public void MoveHandler(Vector3 newHandlePos)
    {
        Vector3 handlePos = _handle.transform.position; 
        if(_lVLController.CurrentSelectedObject == _bar)
        _handle.transform.position = new Vector3(newHandlePos.x, handlePos.y, handlePos.z);
    }

    public void CalculateSliderValue()
    {
        currentValue = maxValue * _handle.transform.position.x/barCollider.bounds.max.x;
        if(currentValue < 0) currentValue = 0;
        if(currentValue > maxValue) currentValue = maxValue;
    }

    void Update()
    {
        CalculateSliderValue();
    }
}
