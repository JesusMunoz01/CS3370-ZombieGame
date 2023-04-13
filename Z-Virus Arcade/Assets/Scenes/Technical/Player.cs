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
    private float JUMP_SPEED = 20.0f;
    private Vector3 checkArea = new Vector3(0.1f, 1f, 0f);
    public Interactable focus;
    public float searchTimer = 5f;
    public bool searchPressed = false;

    void Start()
    {
        view = Camera.main;
        m_rb = GetComponent<Rigidbody>();
        m_cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(m_cc.isGrounded){
            m_vel = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
            
            m_vel = m_vel * 10.0f;

            if(Input.GetAxis("Jump") > 0){
                m_vel += m_vel + (Vector3.up * JUMP_SPEED);
            }

        }else{
                 m_vel += (Vector3.down * GRAVITY * Time.deltaTime);
        }
        //m_vel = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //m_vel = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        //m_cc.Move(m_vel * 10.0f * Time.deltaTime);

        //m_rb.AddForce(m_vel * 10.0f, ForceMode.Acceleration);
        if(Input.GetKey(KeyCode.Q)){
            transform.Rotate(Vector3.down * 100 * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.E)){
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.G)){
        //if(Input.GetMouseButton(1)){
            Ray ray = view.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100)){
                    RemoveFocus();
                }
            }

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

        if(searchPressed == true){
            searchTimer -= Time.deltaTime;
            Debug.Log(searchTimer);

            if(searchTimer <= 0){
                RandomEvent();
                searchPressed = false;
                RemoveFocus();
                searchTimer = 5f;
            }   
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

    void RandomEvent(){
        string[] events = {"-10Hp", "+10Hp", "+100 ammo", "Nice luck!"};
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


