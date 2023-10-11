using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadLvl(int lvl)
    {
        if(lvl == 1) 
            GameManager.Instance.CurrentSection = GameSection.Lvl1;
        else
            GameManager.Instance.CurrentSection =  GameSection.Lvl2;
            
        GameManager.Instance.LoadARScene();
    }
}
