using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slider : MonoBehaviour
{
    float minValue = 0, maxValue = 1, currentValue;
    [SerializeField] GameObject _bar, _handle, _maxPoint, _minPoint;
    [SerializeField] LVLController _lVLController;
    public bool IsSelected { set => IsSelected = value; get => IsSelected;}

    // Start is called before the first frame update
    void Start()
    {
        _handle.transform.position = _minPoint.transform.position;
    }

    // Update is called once per frame
    public void MoveHandler(float direction)
    {
        if(_lVLController.CurrentSelectedObject != _handle) return;
        direction = Mathf.Sign(direction);
        if(Vector3.Distance(_maxPoint.transform.position, transform.position) > 0
        && Vector3.Distance(_minPoint.transform.position, transform.position) > 0)
        transform.localPosition += Vector3.left * direction * Time.deltaTime;
    }
}
