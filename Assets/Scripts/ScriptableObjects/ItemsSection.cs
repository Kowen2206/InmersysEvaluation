using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

[CreateAssetMenu (fileName = "Section", menuName = "Data/Menu/ItemSection")]

public class ItemsSection : ScriptableObject
{

    [Serializable]
    public struct Item
    {
        public Texture Icon;
        public MenuSections Section;
        public GameObject Model;
    }

    public Texture Icon;
    public MenuSections Section;
    public List<Item> Items;

    void Awake()
    {
        foreach (Item item in Items)
        {
            item.Model.name = item.Section.ToString();
        }   
    }

}
