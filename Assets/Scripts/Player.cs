using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private GameInput _gameInput;
    
    private bool isWalking;
    private Vector3 lastInteractionDirection;

    [SerializeField] private LayerMask countersLayerMask;
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() => isWalking;

    void HandleInteractions()
    {
        var input = _gameInput.GetMovementNormalized();
        var moveDirection = new Vector3(input.x, 0, input.y);
        float interactionDistance = 2f;
        if (moveDirection != Vector3.zero)
            lastInteractionDirection = moveDirection;

        if (Physics.Raycast(transform.position, lastInteractionDirection, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void HandleMovement()
    {
        var input = _gameInput.GetMovementNormalized();
        
        
        var moveDirection = new Vector3(input.x, 0, input.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // Attempt X movement
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // can move just in x dir
                moveDirection = moveDirX;
            }
            else
            {
                // cant move in x
                // attempt z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move in z
                    moveDirection = moveDirZ;
                }
                else
                {
                    // cant move in any direction
                }
            }
        }

        if (canMove)
            transform.position += moveDirection * moveDistance;

        isWalking = moveDirection != Vector3.zero;

        const float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
}
