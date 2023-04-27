using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchedObject : MonoBehaviour
{
    public void Disable()
    {
        foreach (Transform child in transform)
        {
            Interactable script = child.GetComponent<Interactable>();
 
            if (script != null)
            {
                script.enabled = false;
            }
        }
    }
}
