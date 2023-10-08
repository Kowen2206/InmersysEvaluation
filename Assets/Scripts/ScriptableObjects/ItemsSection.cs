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
        public string Name;
        public Texture Icon;
        public MenuSections Section;
        public GameObject Mesh;
    }

    public string Name;
    public Texture Icon;
    public MenuSections Section;
    public List<Item> Items;

}
