using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Interact()
    {
        Debug.Log("Interacting");
    }
}
