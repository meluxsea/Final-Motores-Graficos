using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("General")]
    
    
    public LayerMask hitt;
    public GameObject Bulletcircle;


    [Header("Shoot!! ")]

    public float RangeFire = 200;

    public float damage = 10f;
    
    private Transform CamPlayerTransform;

    private void Start()
    {
        CamPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }


    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(CamPlayerTransform.position, CamPlayerTransform.forward, out hit, RangeFire, hitt))
            {
                GameObject bulletCircleClone = Instantiate(Bulletcircle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bulletCircleClone, 2f);

                Enemy Target = hit.transform.GetComponent<Enemy>();

                if(Target != null )
                {
                    Target.TakeDamage(damage);
                }
            }
        }



    }
}
