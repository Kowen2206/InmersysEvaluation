using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ARMenu : MonoBehaviour
{
    //Todo: 
    //crear scriptableObject MenuSettings
    //Crear el prefab BackgroundPlane (un plano que ajusta su tamaño al contenido y funciona como background)

    //Settings
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private float 
    _itemsOffsetX, _itemsOffsetY, _bodyOffset, _headerOffset, _headerButtonMargin;
    [SerializeField] private List<ItemsSection> _sections;
    private List<GameObject> items = new List<GameObject>();
    private bool sectionsAreLoaded;
    private float columns;
    private GameObject currentSectionObj;

    // Start is called before the first frame update
    void Start()
    {
        columns = _sections.Count;
        LoadSections();
        LoadItems();
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
            newItem = Instantiate(_itemPrefab, nextPosition, Quaternion.identity);
            newItem.transform.SetParent(transform);
            newItem.GetComponent<ItemPrefab>().Section = section;
            itemCollider = newItem.GetComponent<BoxCollider>();
            itemPosition = newItem.transform.position;

            if(i == 0) currentSectionObj = newItem;
            
            itemPosition.x -= itemCollider.bounds.size.x + _itemsOffsetX;
            nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
            i++;
        }   
    }

    public void LoadItems()
    {
        GameObject newItem;
        Vector3 itemPosition = transform.position;
        BoxCollider itemCollider = _itemPrefab.GetComponent<BoxCollider>();
        itemPosition.z += itemCollider.bounds.size.z + _headerButtonMargin;
        Vector3 nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
        
        //i aumenta 1 por cada item, j aumenta 1 por columna. 
        int i = 1;

        foreach(ItemsSection.Item item in currentSectionObj.GetComponent<ItemPrefab>().Section.Items)
        {
            newItem = Instantiate(_itemPrefab, nextPosition, Quaternion.identity);
            newItem.GetComponent<ItemPrefab>().Item = item;
            newItem.transform.SetParent(transform);
            itemCollider = newItem.GetComponent<BoxCollider>();
            itemPosition = newItem.transform.position;

            itemPosition.x -= itemCollider.bounds.size.x + _itemsOffsetX;

            if(i == 4)
            {
                itemPosition.z += itemCollider.bounds.size.z + _itemsOffsetY;
                itemPosition.x = transform.position.x;
                i = 0;
            }

            nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);

            i++;
        }
    }
}
