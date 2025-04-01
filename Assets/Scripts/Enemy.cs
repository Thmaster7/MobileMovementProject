using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    

    public Animator anim;
    public float stunTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GameObject hand = GameObject.Find("Hand"); // Encuentra la mano del jugador
        Collider handCollider = hand.GetComponent<Collider>();
        handCollider.isTrigger = true; // Asegura que sea un trigger

        Rigidbody handRb = hand.GetComponent<Rigidbody>();
        handRb.isKinematic = true; // Evita que la física lo mueva
    }
    // Update is called once per frame
    void Update()
    {
        
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
        //StartCoroutine(Stun());
    }

    /*private IEnumerator Stun()
    {
        // Desactiva el movimiento del enemigo (si tienes un AI)
        if (TryGetComponent(out EnemyMovement movement))
        {
            movement.enabled = false;
        }

        yield return new WaitForSeconds(stunTime);

        // Reactiva el movimiento después del stun
        if (TryGetComponent(out EnemyMovement movement2))
        {
            movement2.enabled = true;
        }
    }
    internal class EnemyMovement
    {
        public bool enabled { get; internal set; }
    }*/

}
