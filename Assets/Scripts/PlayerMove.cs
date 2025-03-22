using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    private CharacterController controller;
    public Animator anim;
    private float x, y;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * SpeedMove*Time.deltaTime);
        anim.SetFloat("VelX", joystick.Horizontal);
        anim.SetFloat("VelY", joystick.Vertical);
    }
}
