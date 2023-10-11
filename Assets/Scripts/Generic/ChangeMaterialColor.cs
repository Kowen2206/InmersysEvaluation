using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private Color _secondColor;
    private Color baseColor;

    void Awake()
    {
        baseColor = _material.color;
    }

    public void ChangeColor()
    {
        _material.color = baseColor == _material.color? _secondColor : baseColor;
    }
}
