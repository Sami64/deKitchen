using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private bool isWalking;
    
    private void Update()
    {
        var input = new Vector2(0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            input.y = +1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.y = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x = +1;
        }

        input = input.normalized;
        var moveDirection = new Vector3(input.x, 0, input.y);
        transform.position += moveDirection * (Time.deltaTime * moveSpeed);

        isWalking = moveDirection != Vector3.zero;

        const float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking() => isWalking;
}
