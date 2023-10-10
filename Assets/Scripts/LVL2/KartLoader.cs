using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KartLoader : MonoBehaviour
{
    private GameObject _kart, _wheel, _character, _wheelsParent;
    [SerializeField] private PlayerKart _playerKart;
    private float wheelHeigth;
    
    public void LoadPlayerKartData()
    {
        _playerKart.LoadData();
        LoadKart(_playerKart.Kart);
    }

    public void SavePlayerKartData()
    { 
        _playerKart.Wheel = _wheel;
        _playerKart.Character = _character;
        _playerKart.SaveData();
    }
    
    public void LoadPart(GameObject model, MenuSections part)
    {
        if(part == MenuSections.Karts) { LoadKart(model); return; }
        
        foreach (Transform pivot in _kart.transform)
        {
            if(pivot.name == part.ToString() + "Pivot")
            {
                if(part != MenuSections.Wheels)
                {
                    LoadOneMeshObject(model, pivot);
                }
                else
                    LoadWheels(pivot, model);
            }
        }
    }

    void LoadKart(GameObject model)
    {
        if(!_wheel) _wheel = _playerKart.Wheel;
        if(!_character) _character = _playerKart.Character;
        if(_kart) _kart.GetComponent<DestroyObj>().Destroy();
        _kart = Instantiate(model, transform);
        LoadPart(_wheel, MenuSections.Wheels);
        LoadPart(_character, MenuSections.Characters);

    }

    void LoadWheels(Transform wheelsParent, GameObject wheel)
    {
        _wheelsParent = wheelsParent.gameObject;
        foreach (Transform pivotwheel in wheelsParent)
        {
            if(pivotwheel.childCount > 0)
            {
                pivotwheel.GetChild(0).GetComponent<DestroyObj>().Destroy();
            }
            GameObject modelInstance = Instantiate(wheel, pivotwheel);
            modelInstance.transform.localPosition = Vector3.zero;
        }
        wheelHeigth = wheel.GetComponent<BoxCollider>().bounds.size.y;
        SetKartPosition();
    }

    public void LoadOneMeshObject(GameObject model, Transform parent)
    {
        if(parent.childCount > 0) parent.GetChild(0).GetComponent<DestroyObj>().Destroy();
        GameObject modelInstance = Instantiate(model, parent);
        modelInstance.transform.localPosition = Vector3.zero;
    }

    void SetKartPosition()
    {
        _kart.transform.position = 
        new Vector3(transform.position.x, transform.position.y + wheelHeigth/2, transform.position.z);
    }

}
