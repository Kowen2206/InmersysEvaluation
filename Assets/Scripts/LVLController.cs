using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLController : MonoBehaviour
{

    [SerializeField] private GameObject lVL1Group, lVL2Group;

    void Start()
    {
        if(GameManager.Instance.CurrentSection == GameSection.Lvl1)
        {
            lVL2Group.SetActive(false);
            lVL1Group.SetActive(true);
        }
        else
        {
            lVL2Group.SetActive(true);
            lVL1Group.SetActive(false); 
        }
    }


}
