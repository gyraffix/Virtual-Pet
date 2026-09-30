using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class CreatureWander : MonoBehaviour
{
    private float timer;

    [SerializeField]
    private float wanderTimer;
    private float wanderRadius = 7f;

    private Animator creatureAnimator;
    private NavMeshAgent agent;

    bool canWander = true;
    bool started = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        creatureAnimator = gameObject.GetComponent<Animator>();
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void StopWandering()
    {
        canWander = false;
    }


    public void StartWandering()
    {
        canWander = true;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

        // Update is called once per frame
        void Update()
    {
        timer += Time.deltaTime;

        if (canWander && timer > wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            creatureAnimator.SetTrigger("StartWalking");
            timer = 0;
            started = true;
        }

        
        if(started && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {

            creatureAnimator.SetTrigger("StartIdling");
        }


        

        
    }
}
