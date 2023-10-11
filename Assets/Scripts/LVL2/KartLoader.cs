using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class KartLoader : MonoBehaviour
{
    [SerializeField] private PlayerKart _playerKart;
    [Range(0,1f)][SerializeField] private float _spawnYOffset = .2f;
    [SerializeField] UnityEvent _onSpawnKart, _onSpwnWheels, _onSpawnCharacter;
    private GameObject _kart, _wheel, _character, _wheelsParent;
    private float wheelHeigth;
    public Transform SpawnPoint {get; set;}
    
    public void LoadPlayerKartData()
    {
        _playerKart.DeleteData();
        _playerKart.LoadData();
        LoadKart(_playerKart.Kart);
    }

    public void SavePlayerKartData(GameObject model, MenuSections part)
    { 
        if(part == MenuSections.Wheels) _playerKart.Wheel = model;
        if(part == MenuSections.Characters) _playerKart.Character = model;
        if(part == MenuSections.Karts) _playerKart.Kart = model;
        _playerKart.SaveData();
    }
    
    public void LoadPart(GameObject model, MenuSections part)
    {
        SavePlayerKartData(model, part);
        if(part == MenuSections.Karts) 
        { 
            LoadKart(model); 
            return; 
        }
        
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
        _kart = Instantiate(model, SpawnPoint);
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
        float YPosition = SpawnPoint.transform.position.y + wheelHeigth/2;
        YPosition += YPosition * _spawnYOffset;
        _kart.transform.position =  
        new Vector3(SpawnPoint.transform.position.x, YPosition, SpawnPoint.transform.position.z);
    }

}
