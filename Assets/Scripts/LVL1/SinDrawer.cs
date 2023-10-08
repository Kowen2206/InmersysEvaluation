using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinDrawer : MonoBehaviour
{
    [SerializeField] private int resolution = 10;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float length = 2f;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _graphicObject;
    [SerializeField] private Slider _slider;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawSin();
    }

    void DrawSin()
    {
        _lineRenderer.positionCount = resolution + 1;
        Vector3[] positions = new Vector3[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float x = length * i / resolution;
            float y = amplitude * Mathf.Sin(frequency * x);
            positions[i] = new Vector3(x, y, 0);
        }

        _lineRenderer.SetPositions(positions);
    }

    public void SetObjectInGraphic()
    {
        int positionIndex =
        Mathf.Clamp(
            Mathf.RoundToInt(_lineRenderer.positionCount * _slider.CurrentValue / _slider.MaxValue),
            0, _lineRenderer.positionCount - 1);
        _graphicObject.transform.localPosition = _lineRenderer.GetPosition(positionIndex);
    }
}
