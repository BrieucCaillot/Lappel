using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PenguinController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    [NonSerialized]
    public GameObject destinations = null;

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
        Invoke("RandomCry", Random.Range(10, 20));
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

    private void RandomCry()
    {
        anim.SetTrigger("StartBeak"); 

        float randomTime = Random.Range(10, 25);
        Invoke("RandomCry", randomTime);
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
