using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10f;
    public float jumpPower = 15f;
    public float turnSpeed = 7f;
    public Transform[] rayStartPoint;
    GameManager gameManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }
   

    void Update()
    {
        if (!gameManager.GetLevelFinish)
        {
            MovementSystem();

        }

    }
    private void MovementSystem()
    {
        if(Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            rb.velocity = new Vector3(rb.velocity.x,Mathf.Clamp((jumpPower * 100) * Time.deltaTime , 0f, 15f), 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {

            rb.velocity = new Vector3(Mathf.Clamp((-speed * 100) * Time.deltaTime, -15f, 0f), rb.velocity.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 180f, 0f), turnSpeed * Time.deltaTime);

        }
        else if (Input.GetKey("d"))
        {
            rb.velocity = new Vector3(Mathf.Clamp((speed * 100) * Time.deltaTime, 0 , 15f ) , rb.velocity.y , 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation , Quaternion.Euler(0f, 360f, 0f) , turnSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }

    bool OnGroundCheck()
    { bool hit = false;

        for(int i = 0; i < rayStartPoint.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoint[i].position, -rayStartPoint[i].transform.up, 0.50f);
            Debug.DrawRay(rayStartPoint[i].position, -rayStartPoint[i].transform.up  * 0.50f, Color.green);
        }

        if(hit)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
}
