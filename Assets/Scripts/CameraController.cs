using System.Collections;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    public PlayerMove player;

    public float rotationSmoothSpeed = 5f;
    public float positionSmoothSpeed = 5f;
    
    public Vector3 offset = new Vector3(0, 1, -5);
    public float smoothSpeed = 0.125f;
    
    
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (player.anim.GetInteger("ComboStep") == 0)
        {
            
            Vector3 desiredPosition = player.transform.position + player.transform.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed * Time.deltaTime);
            Quaternion carRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, carRotation, rotationSmoothSpeed * Time.deltaTime);
            
        }
    }

    
}
