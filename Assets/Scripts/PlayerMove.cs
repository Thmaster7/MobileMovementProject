using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    private int comboCount;
    public bool canAttack = true;
    private float comboTimer = 0.5f;
    public float stunTime = 1f;
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    public float rotationSpeed = 1f;
    private CharacterController controller;
    public Animator anim;
    public Button button;
    public Button jumpButton;
    private bool isStunned;
    private bool isOnFire;
    private bool isInFireZone = false;
    public float attackPower;

    private Coroutine comboResetCoroutine;

    public float playerHealth;
    public float maxHealth;

    public Enemy enemy;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        button.onClick.AddListener(Attack);
        jumpButton.onClick.AddListener(Dance);

        

    }
    private void Update()
    {

        Move();
        
        
            
        
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack3") && stateInfo.normalizedTime >= 0.9f)
        {
            ResetToIdle();
        }
        


    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHand")) // Asegúrate de que la mano tenga esta tag
        {
            TakeHit();
            playerHealth -= enemy.attackPower;
        }
        if (other.CompareTag("Fire"))
        {
            isInFireZone = true;
            if (!isOnFire)
            {
                StartCoroutine(OnFire());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Fire"))
        {
            isInFireZone = false;
            isOnFire = false;
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

        yield return new WaitForSeconds(stunTime);

        isStunned = false;
    }

    private IEnumerator OnFire()
    {
        isOnFire = true;
        while (isInFireZone) // Mientras esté en el fuego
        {
            playerHealth -= 1f; // Resta vida constantemente
            yield return new WaitForSeconds(0.5f); // Cada 2 segundos
        }
        isOnFire = false;
    }
    private void Move()
    {
        Vector3 inputDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if (inputDirection.magnitude >= 0.1f && !isStunned)
        {
            // Calcular ángulo de rotación según la dirección del joystick
            float targetAngle = Mathf.Atan2(-inputDirection.x, -inputDirection.z) * Mathf.Rad2Deg;

            // Rotar suavemente hacia esa dirección
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Mover en esa dirección
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * SpeedMove * Time.deltaTime);
        }

        anim.SetFloat("VelX", joystick.Horizontal);
        anim.SetFloat("VelY", joystick.Vertical);

        
    }
    


    private void Attack()
    {
        if (!canAttack) return;

        canAttack = false;
        comboCount++;

        if (comboCount > 3) comboCount = 1;

        anim.SetInteger("ComboStep", comboCount);
        anim.SetTrigger("Attack");

        // Permitir el siguiente ataque después de un pequeño tiempo
        StartCoroutine(AllowNextAttack());

        // Si hay un reseteo del combo en proceso, cancelarlo
        if (comboResetCoroutine != null)
        {
            StopCoroutine(comboResetCoroutine);
        }

        // Iniciar un nuevo reset del combo si no se continúa el ataque en la ventana de tiempo
        comboResetCoroutine = StartCoroutine(ResetAttack());
    }
    private IEnumerator AllowNextAttack()
    {
        yield return new WaitForSeconds(0.2f); // Ajusta según la duración del ataque
        canAttack = true;
    }
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(comboTimer);

        // Si el combo está en Attack3, dejamos que vuelva solo
        if (comboCount < 3)
        {
            ResetToIdle();
        }
    }

    private void ResetToIdle()
    {
        comboCount = 0;
        anim.SetInteger("ComboStep", 0);
        canAttack = true;
    }
    private void Dance()
    {
        anim.SetTrigger("Dance");
        
        
        
    }
    

}
