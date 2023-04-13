using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float area = 2f;
    bool isSearching = false;
    Transform player;

    void Update(){
        if(isSearching){
            float radius = Vector3.Distance(player.position, transform.position);
            if(radius <= area){
                Debug.Log("Searching");
            }
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, area);
    }

    public void OnSearch(Transform playerT){
        isSearching = true;
        player = playerT;
    }

    public void OnDone(){
        isSearching = false;
        player = null;
    }
    
}
