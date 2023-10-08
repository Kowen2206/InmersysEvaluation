using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSectionPrefab : InteractiveObject
{
    private MeshRenderer _meshRenderer;
    public ItemsSection Section{ get; set; }

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        LoadIcon();
    }

    public void LoadIcon()
    {
        Material newMaterial = new Material(Shader.Find("Standard"));
        newMaterial.mainTexture = Section.Icon;
        _meshRenderer.material = newMaterial;
    }

    public override void Interact()
    {
        base.Interact();

        LVLController.Instance.ARMenu.currentSelectedSection = this.gameObject;
        LVLController.Instance.ARMenu.LoadItems();
    }
}
