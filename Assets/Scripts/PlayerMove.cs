using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class PlayerMove : MonoBehaviour
{
    

    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    public float rotationSpeed = 1f;
    private CharacterController controller;
    public Animator anim;
    public Button button;
    private bool isAttacking;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        button.onClick.AddListener(Attack);
        
    }
    private void Update()
    {
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * SpeedMove*Time.deltaTime);
        anim.SetFloat("VelX", joystick.Horizontal);
        anim.SetFloat("VelY", joystick.Vertical);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationY = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotationY, 0);
            }
        }
        

    }
    
    private void Attack()
    {
        if(anim.GetBool("Attack") == false)
        {
            anim.SetBool("Attack", true);
            
            StartCoroutine(ResetAttack());
            
        }
        
        







    }
    
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2f); 
        anim.SetBool("Attack", false);
        
    }

}
