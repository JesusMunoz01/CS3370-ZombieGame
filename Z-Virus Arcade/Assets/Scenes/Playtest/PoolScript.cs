using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolScript : MonoBehaviour
{
    public WeaponSwitch item;
    public Weapons weaponTest;
    public int currentItem = 0;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    public void SelectDrop()
    {
        weaponTest = GetComponentInChildren<Weapons>();

        int index = 0;
        foreach(Transform weapon in transform)
        {
            if(index == currentItem)
                weapon.transform.SetParent(GameObject.Find("Weapons").transform, false);

            index++;
        }
        Debug.Log("Found Weapon");
    }
}

