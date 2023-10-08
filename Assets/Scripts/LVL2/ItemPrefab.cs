using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class ItemPrefab : MonoBehaviour
{
    [SerializeField] private Material _material;
    public string Name{ get; set;}
    public MenuSections section{ get; set;}
    public GameObject Mesh{get; set;}

    public void SetupItemSectionPrefab(ItemsSection data)
    {
        Name = data.Name;
        LoadIcon(data.Icon);
        section = data.Section;
    }

    public void SetupItemPrefab(ItemsSection.Item data)
    {
        Name = data.Name;
        LoadIcon(data.Icon);
        section = data.Section;
        Mesh = data.Mesh;
    }

    public void LoadIcon(Texture icon)
    {
        _material.mainTexture = icon;
    }
}