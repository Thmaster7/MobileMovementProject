using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class PlayerMove : MonoBehaviour
{
    private int comboCount;
    public bool canAttack = true;
    private float comboTimer = 0.5f;
    
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    public float rotationSpeed = 1f;
    private CharacterController controller;
    public Animator anim;
    public Button button;
    public Button jumpButton;

    private Coroutine comboResetCoroutine;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        button.onClick.AddListener(Attack);
        jumpButton.onClick.AddListener(Dance);

        
    }
    private void Update()
    {
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * SpeedMove*Time.deltaTime);
        anim.SetFloat("VelX", joystick.Horizontal);
        anim.SetFloat("VelY", joystick.Vertical);
        
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationY = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotationY, 0);
            }
        
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack3") && stateInfo.normalizedTime >= 0.9f)
        {
            ResetToIdle();
        }

        

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
