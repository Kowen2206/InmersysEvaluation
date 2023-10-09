using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLController : MonoBehaviour
{
    public static LVLController Instance;

    [SerializeField] private GameObject _lVL1Group, _lVL2Group;
    [SerializeField] private ARMenu _aRMenu;
    [SerializeField] private KartLoader _kartLoader;
    
    public KartLoader KartLoader{get => _kartLoader;}
    public ARMenu ARMenu{get => _aRMenu;}
    private GameObject currentSelectedObject;

    public GameObject CurrentSelectedObject
    {
        get => currentSelectedObject;
        set => currentSelectedObject = value;
    }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if(GameManager.Instance.CurrentSection == GameSection.Lvl1)
            LoadLVL1();
        else
            LoadLVL2();
        
    }

    public void LoadLVL1()
    {
        _lVL2Group.SetActive(false);
        _lVL1Group.SetActive(true);
    }

    public void LoadLVL2()
    {
        _lVL2Group.SetActive(true);
        _lVL1Group.SetActive(false);
        _kartLoader.LoadPlayerKartData();
        _aRMenu.LoadSections();
        _aRMenu.LoadItems();
    }

    public void InteractWithSelectedObj()
    {
        if(currentSelectedObject)
        currentSelectedObject.GetComponent<InteractiveObject>().Interact();
    }

}
