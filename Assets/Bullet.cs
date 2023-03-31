using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = -10f;
    public GameObject owner;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.GetComponent<Destroy>() == false)
        {
            Destroy(gameObject);
        }
    }
}
