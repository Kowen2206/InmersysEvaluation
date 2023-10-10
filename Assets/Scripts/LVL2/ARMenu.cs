using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ARMenu : MonoBehaviour
{
    //Todo: 
    //Crear el prefab BackgroundPlane (un plano que ajusta su tamaño al contenido y funciona como background)

    //Settings
    [SerializeField] private GameObject _itemPrefab, _itemSectionPrefab;
    [Range(0,.2f)][SerializeField] private float 
    _itemsOffsetX, _sectionsOffsetX, _itemsOffsetY, _headerOffset, _headerButtonMargin;
    [SerializeField] private List<ItemsSection> _sections;
    [SerializeField] private bool _invertSpawnDirection;
    private List<GameObject> items = new List<GameObject>();
    private bool sectionsAreLoaded;
    private float columns;
    public GameObject currentSelectedSection{get; set;}
    private Vector3 menuFooterPosition;
    private Dictionary<MenuSections, GameObject> itemsContainers = new Dictionary<MenuSections, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        columns = _sections.Count;
    }

    public void LoadSections()
    {
        if(sectionsAreLoaded) return;

        GameObject newItem;
        Vector3 nextPosition = transform.position;
        BoxCollider itemCollider;
        Vector3 itemPosition;
        int i = 0;

        foreach (ItemsSection section in _sections)
        {
            
            newItem = Instantiate(_itemSectionPrefab, nextPosition, Quaternion.identity);
            newItem.transform.SetParent(transform);
            newItem.GetComponent<ItemSectionPrefab>().Section = section;
            itemCollider = newItem.GetComponent<BoxCollider>();
            itemPosition = newItem.transform.position;

            if(i == 0) currentSelectedSection = newItem;
            
            if(i == columns)
            {
                menuFooterPosition.x = itemPosition.x/2;
            }
            if(_invertSpawnDirection)
            {
                newItem.transform.localRotation = Quaternion.identity;
                itemPosition.x += _sectionsOffsetX;
            }
            else
                itemPosition.x -= _sectionsOffsetX;
            nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);

            i++;
        }   
    }

    public void LoadItems()
    {
        MenuSections currentSection = currentSelectedSection.GetComponent<ItemSectionPrefab>().Section.Section;

        if(itemsContainers.ContainsKey(currentSection))
        {
            ShowItems(currentSection);
            return;
        }
        CreateItemsSection(currentSection);

        GameObject newItem;
        Vector3 itemPosition = transform.position;
        BoxCollider itemCollider = _itemPrefab.GetComponent<BoxCollider>();
        if(_invertSpawnDirection)
            itemPosition.z -= _headerButtonMargin;
        else
            itemPosition.z += _headerButtonMargin;
        
        Vector3 nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
         
        int i = 1;
        int itemsCount = currentSelectedSection.GetComponent<ItemSectionPrefab>().Section.Items.Count;
        foreach(ItemsSection.Item item in currentSelectedSection.GetComponent<ItemSectionPrefab>().Section.Items)
        {
            newItem = Instantiate(_itemPrefab, nextPosition, Quaternion.identity);
            newItem.GetComponent<ItemPrefab>().Item = item;
            newItem.transform.SetParent(itemsContainers[currentSection].transform);
            
            itemCollider = newItem.GetComponent<BoxCollider>();
            itemPosition = newItem.transform.position;
            if(_invertSpawnDirection)
            {
                newItem.transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
                itemPosition.x += _itemsOffsetX;
            }
            else
                itemPosition.x -= _itemsOffsetX;
            if(i == columns)
            {
                if(_invertSpawnDirection)
                    itemPosition.z -= _headerButtonMargin;
                else
                    itemPosition.z += _headerButtonMargin;

                itemPosition.x = transform.position.x;
                i = 0;
            }

            if(i == itemsCount)
            {
                menuFooterPosition.y = itemPosition.y + itemCollider.bounds.extents.y + _headerOffset;
            }
            
            nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
            i++;
        }
        ShowItems(currentSection);
    }

    void CreateItemsSection(MenuSections itemSection)
    {
        if(!itemsContainers.ContainsKey(itemSection))
        {
            itemsContainers.Add(itemSection, new GameObject());

            Debug.Log("CreatedSection");
            Debug.Log(itemSection);
        }
            
    }

    void ShowItems(MenuSections section)
    {
        foreach (MenuSections itemSection in itemsContainers.Keys)
        {
           if(itemSection == section)
            itemsContainers[itemSection].SetActive(true);
           else
            itemsContainers[itemSection].SetActive(false);
        }
    }
}
