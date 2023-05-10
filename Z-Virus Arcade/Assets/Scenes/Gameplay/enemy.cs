using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public Transform soldier;
    public HealthBar healthb;
    int moveSpeed = 2;
    int maxDist = 101;
    int minDist = 5;
    public int health = 50;
    public float cooldown = 5;
    public float cooldownTimer = 5;
    public int enemyDamage = 5;

/*
    public static event Action<enemy> EnemyKilled;
    [SerializeField] float health, maxHealth = 1f;

    [SerializeField] float moveSpeed = 8f;
    Rigidbody rb;
    public Transform soldier;
    Vector2 moveDirection;

    private void Begin()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        soldier = GameObject.Find("soldier").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(soldier){
            Vector3 direction = (soldier.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = new Quaternion (angle, 0, 0, 0);
            moveDirection = direction;
        }

    }

    void FixedUpdate()
    {
        if(soldier)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
    */
    void Start()
    {
        healthb.MaxHealth(health);
    }
    void FixedUpdate()
    {
        transform.LookAt(soldier);
        if(cooldownTimer > -0.1){
            cooldownTimer -= Time.deltaTime;
        }
        if(health > 0)
        {
            if (Vector3.Distance(transform.position, soldier.position) <= maxDist)
            {
                if (Vector3.Distance(transform.position, soldier.position) <= minDist)
                {
                    if(cooldownTimer <= 0){
                        Player user = soldier.GetComponent<Player>();
                        user.TakeDamage(enemyDamage);
                        cooldownTimer = cooldown;
                    }
                }
                else
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                }
            }
        }
        else{Destroy(gameObject);}

    }

    public void DamageEnemy(int amnt){
        health -= amnt;
        healthb.Health(health);
    }
}
