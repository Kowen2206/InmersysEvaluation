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
            //newItem.GetComponent<ItemPrefab>().SetupItemSectionPrefab(section);
            itemCollider = newItem.GetComponent<BoxCollider>();
            itemPosition = newItem.transform.position;

            if(i == 0)
            {
                itemPosition.x += itemCollider.bounds.size.x + _itemsOffsetX;
                nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
            }
            else
            {
                itemPosition.x = itemPosition.x + (itemCollider.bounds.size.x * i) + _itemsOffsetX;
                nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z) ;
            }
        }   
    }

    public void LoadItems()
    {
        GameObject newItem;
        Vector3 itemPosition = transform.position;
        BoxCollider itemCollider = _itemPrefab.GetComponent<BoxCollider>();
        itemPosition.z += itemCollider.bounds.size.y + _headerButtonMargin;
        Vector3 nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
        
        //i aumenta 1 por cada item, j aumenta 1 por columna. 
        int i = 0, j = 0;

        foreach(ItemsSection.Item item in currentSectionObj.GetComponent<ItemsSection>().Items)
        {
            newItem = Instantiate(_itemPrefab, nextPosition, Quaternion.identity);
            //newItem.GetComponent<ItemPrefab>().SetupItemPrefab(item);
            itemCollider = newItem.GetComponent<BoxCollider>();
            itemPosition = newItem.transform.position;

            if(i == 0)
            {
                itemPosition.x += itemCollider.bounds.size.x + _itemsOffsetX;
                nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
            }
            else
            {
                itemPosition.x = itemPosition.x + (itemCollider.bounds.size.x * i) + _itemsOffsetX;
                nextPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z) ;
            }

            if(j%4 == 0)
            {
                itemPosition.z += itemCollider.bounds.size.y + _itemsOffsetY;
                i = 0;
            }
        }
    }
}
