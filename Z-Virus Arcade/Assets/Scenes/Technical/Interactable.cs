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
    public searchTimerText text;
    public Player user;
    public float searchTimer = 6f;

    void Start(){
        GameObject timerText;
        timerText = GameObject.Find("Timer");
        text = timerText.GetComponentInChildren<searchTimerText>();

        GameObject playerUser;
        playerUser = GameObject.Find("Player");
        user = playerUser.GetComponentInChildren<Player>();
    }

    void Update(){
        if(isSearching){
            float radius = Vector3.Distance(player.position, transform.position);
            if(radius <= area){
                Debug.Log("Searching");
                text.setText("Searching...");
                text.showTimer();
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
                text.endTimer();
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

    public float passTimer(){
        return searchTimer;
    }

    void RandomEvent(){
    string[] events = {"-5Hp", "+10Hp", "Run!", "+100 health!"};
    var chance = Random.Range(1f, 100f);
    if(chance <= 2){
            Debug.Log(events[3]);
            user.GetHealth(100);
            text.setText(events[3]);
        }
    else if(chance <= 20){
            Debug.Log(events[1]);
            user.GetHealth(10);
            text.setText(events[1]);
        }
    else if(chance <= 60){
            Debug.Log(events[0]);
            user.TakeDamage(5);
            text.setText(events[0]);
        }
    else{
            Debug.Log(events[2]);
            text.setText(events[2]);
        }
    }
    
}
