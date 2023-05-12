using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject zombie;
    public float timer = 10f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f){
            Instantiate(zombie, transform.position, Quaternion.identity);
            timer = 10f;
        }
    }
}
