using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float area = 2f;
    bool isSearching = false;
    bool completeSearch = false;
    Transform player;
    public PoolScript pool;
    public float searchTimer = 6f;

    void Update(){
        if(isSearching){
            float radius = Vector3.Distance(player.position, transform.position);
            if(radius <= area){
                Debug.Log("Searching");
            }
            searchTimer -= Time.deltaTime;
            Debug.Log(searchTimer);

            if(searchTimer <= 0){
                GameObject thing;
                thing = GameObject.Find("LootPool");
                //pool = thing.GetComponent<PoolScript>();
                pool = GetComponentInChildren<PoolScript>();
                pool.SelectDrop();
                RandomEvent();
                completeSearch = true;
                pool = thing.GetComponent<PoolScript>();
                searchTimer = 6f;
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
        this.enabled = false;
        //transform.GetComponentInParent<SearchedObject>().Disable();
    }

    public bool Complete(){
        return completeSearch;
    }

    void RandomEvent(){
    string[] events = {"-10Hp", "+10Hp", "+10 ammo", "Nice luck!"};
    var chance = Random.Range(1f, 100f);
    if(chance <= 2){
            Debug.Log(events[4]);
        }
    else if(chance <= 20){
            Debug.Log(events[1]);
        }
    else if(chance <= 60){
            Debug.Log(events[0]);
        }
    else{
            Debug.Log(events[2]);
        }
    }
    
}
