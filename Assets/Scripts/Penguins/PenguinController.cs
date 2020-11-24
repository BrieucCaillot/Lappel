﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PenguinController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField]
    private GameObject destinations = null;

    private float acceleration = 2f;
    private float deceleration = 10f;
    private int maxDistance = 0;
    private bool reachedDestination = false;
    private Vector3 currentDestination;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        maxDistance = Random.Range(3, 8);
        agent.stoppingDistance = maxDistance;
        StartCoroutine(nameof(ReachDestination));
        Invoke("RandomPause", Random.Range(5, 10));
    }

    private void Update()
    {
        if (!reachedDestination && agent.remainingDistance > 0 && agent.remainingDistance < maxDistance) OnPenguinReachedDestination(); 
    }

    private void PickRandomDestination() 
    {
        var randomId = Random.Range(0, destinations.transform.childCount - 1);
        currentDestination = destinations.transform.GetChild(randomId).GetComponent<NavMeshObstacle>().transform.position;
    }

    private void RandomPause()
    {
        if (agent.remainingDistance > 50)
        {
            agent.isStopped = !agent.isStopped;
            anim.SetTrigger(agent.isStopped ? "StartIdle" : "StartWalking"); 
        }
        
        float randomTime = Random.Range(5, 15);
        Invoke("RandomPause", randomTime);
    }

    IEnumerator ReachDestination()
    {
        var randomSeconds = Random.Range(0f, 7f);
        yield return new WaitForSeconds(randomSeconds);
        
        // print("REACH DESTINATION");
        
        reachedDestination = false;
        PickRandomDestination();
        anim.SetTrigger("StartWalking");
        agent.SetDestination(currentDestination);
    }

    private void OnPenguinReachedDestination()
    {
        // print("OnPenguinReachedDestination");
        anim.SetTrigger("StartIdle");
        reachedDestination = true;
        StartCoroutine(nameof(ReachDestination));
    }
}
