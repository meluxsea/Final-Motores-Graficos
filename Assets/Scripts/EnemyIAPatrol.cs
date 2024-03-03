using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class EnemyIAPatrol : MonoBehaviour
{
    public Transform player;
    public float enemyHealth = 100f;

    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;
       //patroling
       Vector3 destPoint;
        bool walkpointSet;
    [SerializeField] float range;
    
    //states
    [SerializeField] float sightRange, attackRange;
    bool playerSight, playerInAttackRange;



    //Game Over
    public GameObject panelGameOver;

    // Start is called before the first frame update
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        panelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerSight = Physics.CheckSphere(transform.position,sightRange,playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,playerLayer);

        if(!playerSight && !playerInAttackRange) Patrol();
        if(playerSight && !playerInAttackRange) Chase();
        if(playerSight && playerInAttackRange) Attack();


        if(playerInAttackRange)
        {
            HandlePlayerCollision();
        }
    }

    void HandlePlayerCollision()
    {
        panelGameOver.SetActive(true);
        // Puedes detener el tiempo o realizar otras acciones necesarias
        

        // Desactiva los controles del jugador si es necesario
        player.GetComponent<CharacterController>().enabled = false;

        Invoke("ResetGameFirstScene", 2f);
    }

    void ResetGameFirstScene()
    {
        

        SceneManager.LoadScene("Scene01");
    }




    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

       
        
    }

    



    void Patrol()
    {
        if (!walkpointSet) SearchForDest();
        if (walkpointSet)agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkpointSet = false;

        
    }
    void SearchForDest()
    {
        float z = UnityEngine.Random.Range(-range, range);
        float x = UnityEngine.Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if(Physics.Raycast(destPoint,Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

}
