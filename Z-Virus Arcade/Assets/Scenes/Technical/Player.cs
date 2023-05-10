using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody m_rb;
    CharacterController m_cc;
    Vector3 m_vel = Vector3.zero;
    Camera view;

    private float GRAVITY = 10.0f;
    private float JUMP_SPEED = 5.0f;
    private Vector3 checkArea = new Vector3(0.1f, 1f, 0f);
    public Interactable focus;
    public enemy opponent;
    public HealthBar healthBar;
    public WeaponSwitch weapon;
    public PoolScript pool;
    public float searchTimer = 5f;
    public bool searchPressed = false;
    public int health = 100;

    void Start()
    {
        view = Camera.main;
        m_rb = GetComponent<Rigidbody>();
        m_cc = GetComponent<CharacterController>();
        weapon = GetComponentInChildren<WeaponSwitch>();
        healthBar.MaxHealth(health);
    }

    void Update()
    {
        // Character Movement
        if(m_cc.isGrounded){
            m_vel = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
            
            m_vel = m_vel * 10.0f;

            if(Input.GetAxis("Jump") > 0){
                m_vel += m_vel + (Vector3.up * JUMP_SPEED);
            }

        }else{
                 m_vel += (Vector3.down * GRAVITY * Time.deltaTime);
        }

        // Camera rotation with Q and E
        if(Input.GetKey(KeyCode.Q)){
            transform.Rotate(Vector3.down * 100 * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.E)){
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        }

        // Camera rotation with mouse
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * 3, 0);

        // Stop searching
        if(Input.GetKey(KeyCode.G)){
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100)){
                    RemoveFocus();
                }
            }

        // Searching function
        if(Input.GetKeyDown(KeyCode.F)){
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100)){
                Interactable search = hit.collider.GetComponent<Interactable>();
                if (search != null){
                    searchPressed = true;
                    SetFocus(search);
                }
            }

        }

        // Attack & Calculations
        if(Input.GetMouseButtonDown(0)){
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, weapon.getRange())){
                enemy entity = hit.collider.GetComponent<enemy>();
                if (entity != null){
                    SetEnemy(entity);
                }
            }
        }
        
        // Lock mouse
        if(Input.GetMouseButtonDown(1)){
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
        // Searching counter
        if(searchPressed == true){
            //searchTimer -= Time.deltaTime;
            //Debug.Log(searchTimer);

            // if(searchTimer <= 0){
            //     searchPressed = false;
            //     RemoveFocus();
            //     searchTimer = 5f;
            // } 
            if(focus.Complete()){
                searchPressed = false;
                RemoveFocus();
                searchTimer = 5f;
            }     
        }

        // Self damage (Testing purposes)
        if(Input.GetKeyDown(KeyCode.L)){
            TakeDamage(weapon.getDamage());
        }
    }

    void FixedUpdate()
    {
        m_cc.Move(m_vel * Time.deltaTime);
    }

    void SetFocus(Interactable newFocus){
        focus = newFocus;
        newFocus.OnSearch(transform);
    }

    void RemoveFocus(){
        focus.OnDone();
        focus = null;
    }

     void SetEnemy(enemy newFocus){
        opponent = newFocus;
        newFocus.DamageEnemy(weapon.getDamage());
        RemoveEnemy();
    }

    void RemoveEnemy(){
        opponent = null;
    }

    public void TakeDamage(int amnt)
    {
        health -= amnt;
        healthBar.Health(health);
    }

    public void GetHealth(int amnt)
    {
        health += amnt;
        healthBar.Health(health);
    }

}


