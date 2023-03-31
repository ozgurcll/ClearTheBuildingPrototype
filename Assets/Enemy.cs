using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;

    [SerializeField] private bool canMoveRight = false;

    [SerializeField] private float shootRange = 10f;
    [SerializeField] private LayerMask shootLayer;
    private Transform aimTransform;

    [SerializeField] private float reloadTime = 5f;
    [SerializeField] private bool isReloaded = false;

    private Attack attack;
    private void Awake()
    {
        attack = GetComponent<Attack>();
        aimTransform = attack.GetFireTransform;
    }
   
    void Update()
    {
        EnemyAttack();

        CheckCanMoveRight();

        MoveTowards();

        Aim();
    }

    private void Reload()
    {
        attack.GetAmmo = attack.GetClipSize;
    }

    private void EnemyAttack()
    {
        if(attack.GetAmmo <= 0)
        {
            Invoke(nameof(Reload), reloadTime);
            isReloaded = true;
        }

        if (attack.GetCurrentFireRate <= 0f && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
        }
    }

    private void MoveTowards()
    {
        if(Aim() && attack.GetAmmo > 0)
        {
            return;
        }

        if(canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(movePoints[1].position.x , transform.position.y , movePoints[1].position.z), speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 180f, 0f), speed * Time.deltaTime);
        }
        else
        {

            transform.position = Vector3.MoveTowards(transform.position,new Vector3(movePoints[0].position.x , transform.position.y , movePoints[0].position.z), speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 360f, 0f), speed * Time.deltaTime);
        }
    }

    private void CheckCanMoveRight()
    {
        if(Vector3.Distance(transform.position , movePoints[0].position) <= 0.1f)
        {
            canMoveRight = true;
            print("Move Right");
        }
        else if(Vector3.Distance(transform.position , movePoints[1].position) <= 0.1f)
        {
            canMoveRight = false;
            print("Move Left");
        }
    }

  

    private bool Aim()
    {
        if(aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;

        }
        bool hit = Physics.Raycast(aimTransform.position, transform.right, shootRange, shootLayer);
        Debug.DrawRay(aimTransform.position, transform.right * shootRange, Color.blue);
        print("Can Shoot: " + hit);
        return hit;
    }
}
