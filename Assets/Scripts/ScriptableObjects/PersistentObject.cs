using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : ScriptableObject
{
    [SerializeField] protected string _EntityKey;
    public void LoadData()
    {
        if(PlayerPrefs.HasKey(_EntityKey))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(_EntityKey), this);
        }
    }
    
    public void SaveData()
    {
        string scriptData = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(_EntityKey, scriptData);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(_EntityKey);
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}