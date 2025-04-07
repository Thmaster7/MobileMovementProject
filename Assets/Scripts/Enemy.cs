using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    

    public Animator anim;
    public float stunTime = 1f;
    public float detectionRadius = 10f;
    public float attackRange = 1.5f;

    private Transform player;
    private NavMeshAgent agent;
    private bool isStunned;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject hand = GameObject.Find("Hand");
        Collider handCollider = hand.GetComponent<Collider>();
        handCollider.isTrigger = true;

        Rigidbody handRb = hand.GetComponent<Rigidbody>();
        handRb.isKinematic = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isStunned || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            agent.SetDestination(player.position);
            anim.SetBool("isMoving", true);
            

            if (distance <= attackRange)
            {
                anim.SetBool("isMoving", false);
                anim.SetTrigger("Attack");
                agent.ResetPath();
                distance = 0;
            }
            else
            {
                distance = Vector3.Distance(transform.position, player.position);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            agent.ResetPath();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Asegúrate de que la mano tenga esta tag
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        anim.SetTrigger("Hit"); // Activa la animación de recibir golpe
        StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        isStunned = true;
        agent.isStopped = true;
        anim.SetBool("isMoving", false);

        yield return new WaitForSeconds(stunTime);

        isStunned = false;
        agent.isStopped = false;
    }
    

}
