using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class ItemPrefab : InteractiveObject
{

    private MeshRenderer _meshRenderer;
    public ItemsSection.Item Item{ get; set; }

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        LoadIcon();
    }

    public void LoadIcon()
    {
        Material newMaterial = new Material(Shader.Find("Standard"));
        newMaterial.mainTexture = Item.Icon;
        _meshRenderer.material = newMaterial;
    }

    public override void Interact()
    {
        base.Interact();

        switch (Item.Section)
        {
            case MenuSections.Wheels: 
            //LVLController.Instance.KartPrefab.setWheels(Item.Mesh);
            break;
            case MenuSections.Gliders: 
            break;
            case MenuSections.Characters: 
            break;
            case MenuSections.Karts: 
            break;
        }
    }
}