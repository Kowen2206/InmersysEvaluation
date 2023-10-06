using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    float minValue = 0, maxValue = 1, currentValue;
    [SerializeField] GameObject _bar, _handle, _maxPoint, _minPoint;
    float pointsDistance;


    // Start is called before the first frame update
    void Start()
    {
        _handle.transform.position = _minPoint.transform.position;
        pointsDistance = Vector3.Distance(_maxPoint.transform.position, _minPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
