using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class ItemPrefab : MonoBehaviour
{
    [SerializeField] private Material _material;
    public ItemsSection Section{ get; set; }
    public ItemsSection.Item Item{ get; set; }
    public bool IsItem{ get; set; }

    public void LoadIcon(Texture icon)
    {
        _material.mainTexture = icon;
    }
}