using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public void Destroy(float delay = 0)
    {
        Destroy(gameObject, delay);
    }
}
