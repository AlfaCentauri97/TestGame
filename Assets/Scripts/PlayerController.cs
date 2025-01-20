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
    
    [Header("Carry Position Settings")]
    public float carryVerticalSensitivity = 1f;
    private float carryVerticalOffset = 0f;

    private float verticalLookRotation;
    private float currentSpeed;
    private Vector3 velocity;
    private bool isRunning;
    
    private Pickable pickedUpPickable;
    
    private Pickable currentPickable;
    private Item currentItem;
    private Lever currentLever;
    private Door currentDoor;
    private NPCController currentNPC;

    private Pickable previousPickable;
    private Item previousItem;
    private Lever previousLever;
    private Door previousDoor;
    
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
    
    public void TogglePlayerLock(bool isLocked)
    {
        playerLock = isLocked;
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
        currentLever = null;
        currentDoor = null;
        currentNPC = null;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            Pickable pickable = hit.collider.GetComponent<Pickable>();
            Item item = hit.collider.GetComponent<Item>();
            Lever lever = hit.collider.GetComponent<Lever>();
            Door door = hit.collider.GetComponent<Door>();
            NPCController npc = hit.collider.GetComponent<NPCController>();
            //NPC
            if (npc != null)
            {
                currentNPC = npc;
            }
            else if (currentNPC != null)
            {
                currentNPC = null;
            }
            //PICKABLE
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
            //ITEM
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
            //LEVER
            if (lever != null)
            {
                currentLever = lever;

                if (previousLever != currentLever)
                {
                    if (previousLever != null)
                    {
                        previousLever.OutlineAnimation(false);
                    }

                    currentLever.OutlineAnimation(true);
                }

                previousLever = currentLever;
            }
            else if (previousLever != null)
            {
                previousLever.OutlineAnimation(false);
                previousLever = null;
            }
            //DOOR
            
            if (currentPickable != null || currentNPC != null)
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            }
            else if (currentItem != null || currentLever != null)
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
            
            if (previousLever != null)
            {
                previousLever.OutlineAnimation(false);
                previousLever = null;
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
            carryVerticalOffset = 0f;
        }
        else if (pickedUpPickable != null)
        {
            float mouseY = Input.GetAxis("Mouse Y") * carryVerticalSensitivity;
            carryVerticalOffset = Mathf.Clamp(carryVerticalOffset + mouseY, 0f, 1f);
            Vector3 newPosition = carryPosition.localPosition;
            newPosition.y = carryVerticalOffset;
            carryPosition.localPosition = newPosition;
        }
        else if (Input.GetMouseButtonDown(0) && currentItem != null)
        {
            currentItem.PickUp();
        }
        else if (Input.GetMouseButtonDown(0) && currentLever != null)
        {
            currentLever.Interact();
        }
        else if (Input.GetMouseButtonDown(0) && currentNPC != null)
        {
            Debug.Log($"Interacting with NPC: {currentNPC.name}");
            currentNPC.Interact();
        }
        else if (Input.GetMouseButtonDown(0) && currentDoor != null)
        {
            
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
