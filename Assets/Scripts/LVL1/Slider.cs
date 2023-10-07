using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

public class Slider : MonoBehaviour
{

    [SerializeField] GameObject _bar, _handle;
    [SerializeField] LVLController _lVLController;
    [SerializeField] UnityEvent<float> _onSliderValueChange; 

    BoxCollider barCollider;
    private bool isSelected;
    [SerializeField] float maxValue = 100, currentValue;

    public bool IsSelected { set => isSelected = value; get => isSelected;}
    public float MaxValue{ get => maxValue;}
    public float CurrentValue{ get => currentValue;}

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
        _onSliderValueChange?.Invoke(currentValue);
    }

    void Update()
    {
        CalculateSliderValue();
    }
}
