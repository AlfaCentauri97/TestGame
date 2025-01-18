using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    [Header("References")]
    public CharacterController characterController;
    public Transform playerCamera;
    public Transform carryPosition;
    
    [Header("Ray Settings")]
    public float rayDistance = 100f;

    private float verticalLookRotation;
    private float currentSpeed;
    private Vector3 velocity;
    private bool isRunning;

    private Pickable currentPickable;
    private Pickable pickedUpPickable;
    private Item currentItem;

    private Pickable previousPickable;
    private Item previousItem;
    
    public bool playerLock = false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = movementSpeed;

        if (!characterController) characterController = GetComponent<CharacterController>();
        if (!playerCamera) playerCamera = GetComponentInChildren<CinemachineVirtualCamera>().transform;
    }

    private void Update()
    {
        if (!playerLock)
        {
            HandleMovement();
            HandleMouseLook();
            CastAndDrawRay();
            HandleInteraction();   
        }
    }
    
    public void TogglePlayerLock()
    {
        playerLock = !playerLock;
    }
    
    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * currentSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && moveZ > 0)
        {
            if (!isRunning)
            {
                StartCoroutine(ChangeSpeed(runSpeed));
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                StartCoroutine(ChangeSpeed(movementSpeed));
                isRunning = false;
            }
        }
    }
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
    }
    private void CastAndDrawRay()
    {
        int layerMask = ~LayerMask.GetMask("Player");

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        currentPickable = null;
        currentItem = null;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            Pickable pickable = hit.collider.GetComponent<Pickable>();
            Item item = hit.collider.GetComponent<Item>();
            
            if (pickable != null)
            {
                currentPickable = pickable;
                
                if (previousPickable != pickable)
                {
                    if (previousPickable != null)
                        previousPickable.OutlineAnimation(false);
                    currentPickable.OutlineAnimation(true);
                }

                previousPickable = pickable;
            }
            else if (previousPickable != null)
            {
                previousPickable.OutlineAnimation(false);
                previousPickable = null;
            }
            
            if (item != null)
            {
                currentItem = item;
                
                if (previousItem != item)
                {
                    if (previousItem != null)
                        previousItem.OutlineAnimation(false);
                    currentItem.OutlineAnimation(true);
                }

                previousItem = item;
            }
            else if (previousItem != null)
            {
                previousItem.OutlineAnimation(false);
                previousItem = null;
            }
            
            if (currentPickable != null)
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            }
            else if (currentItem != null)
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue);
            }
        }
        else
        {
            if (previousPickable != null)
            {
                previousPickable.OutlineAnimation(false);
                previousPickable = null;
            }

            if (previousItem != null)
            {
                previousItem.OutlineAnimation(false);
                previousItem = null;
            }

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.blue);
        }
    }
    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0) && currentPickable != null && pickedUpPickable == null)
        {
            pickedUpPickable = currentPickable;
            pickedUpPickable.PickedUp(carryPosition);
        }
        else if (Input.GetMouseButtonDown(0) && currentItem != null)
        {
            currentItem.PickUp();
        }
        
        if (Input.GetMouseButtonUp(0) && pickedUpPickable != null)
        {
            pickedUpPickable.OnDrop();
            pickedUpPickable = null;
        }
    }
    private IEnumerator ChangeSpeed(float targetSpeed)
    {
        float startSpeed = currentSpeed;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentSpeed = targetSpeed;
    }
}
