using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float area = 2f;
    bool isSearching = false;
    bool completeSearch = false;
    bool doneSearching = false;
    string eventOutput;
    public Transform player;
    Light itemShine;
    public PoolScript pool;
    public searchTimerText text;
    public Player user;
    public float searchTimer = 6f;
    public float textTimer = 1f;
    public string dispText;
    public bool hasPool = false;
    //public bool isHealingItem = false;

    void Start(){
        GameObject timerText;
        timerText = GameObject.Find("Timer");
        text = timerText.GetComponentInChildren<searchTimerText>();

        GameObject shine;
        shine = GameObject.Find("Shine");
        itemShine = shine.GetComponent<Light>();

        GameObject playerUser;
        playerUser = GameObject.Find("MaleCharacterPolyart");
        user = playerUser.GetComponentInChildren<Player>();

    }

    void Update(){
        // if(isHealingItem == true){
        //     float radius = Vector3.Distance(player.position, transform.position);
        //     if(radius <= area){
        //         Debug.Log("Healing");
        //         text.setText("Healing...");
        //         text.showTimer();
        //         user.GetHealth(1);
        //     }
        //     else
        //         text.endTimer();
        // }

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
                if(hasPool == true){
                    GameObject thing;
                    thing = GameObject.Find("LootPool");
                    pool = GetComponentInChildren<PoolScript>();
                    pool.SelectDrop();
                    pool = thing.GetComponent<PoolScript>();
                }
                RandomEvent();
                doneSearching = true;
                searchTimer = 6f;
                if(dispText != "")
                text.setText(dispText);
                else
                text.setText(eventOutput);
                isSearching = false;
                itemShine.enabled = false;
            }   

        }

        if(doneSearching == true){
            textTimer -= Time.deltaTime;
                if(textTimer <= 0){
                    text.endTimer();
                    textTimer = 1f;
                    completeSearch = true;
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
    string[] events = {"-5Hp", "+10Hp", "Nothing", "+100 health!"};
    var chance = Random.Range(1f, 100f);
    if(chance <= 2){
            Debug.Log(events[3]);
            user.GetHealth(100);
            text.setText(events[3]);
            eventOutput = events[3];
        }
    else if(chance <= 20){
            Debug.Log(events[1]);
            user.GetHealth(10);
            text.setText(events[1]);
            eventOutput = events[1];
        }
    else if(chance <= 60){
            Debug.Log(events[0]);
            user.TakeDamage(5);
            text.setText(events[0]);
            eventOutput = events[0];
        }
    else{
            Debug.Log(events[2]);
            text.setText(events[2]);
            eventOutput = events[2];
        }
    }
    
}
