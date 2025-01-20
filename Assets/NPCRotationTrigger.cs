using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotationTrigger : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public bool isFollowingPlayer = true;

    private Transform playerTransform;
    private Transform parentTransform;
    private Quaternion originalRotation;
    private bool isPlayerInRange = false;

    private void Start()
    {
        parentTransform = transform.parent;

        if (parentTransform != null)
        {
            originalRotation = parentTransform.rotation;
        }
        else
        {
            Debug.LogError("Parent transform is missing. Please ensure this object has a parent.");
        }
    }

    private void Update()
    {
        if (isFollowingPlayer && isPlayerInRange && playerTransform != null && parentTransform != null)
        {
            SmoothFacePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            playerTransform = null;

            if (isFollowingPlayer && parentTransform != null)
            {
                ResetRotation();
            }
        }
    }

    private void SmoothFacePlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - parentTransform.position;
        directionToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        parentTransform.rotation = Quaternion.Slerp(parentTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void ResetRotation()
    {
        if (parentTransform != null)
        {
            StartCoroutine(SmoothResetRotation());
        }
    }

    private IEnumerator SmoothResetRotation()
    {
        while (parentTransform != null && Quaternion.Angle(parentTransform.rotation, originalRotation) > 0.1f)
        {
            parentTransform.rotation = Quaternion.Slerp(parentTransform.rotation, originalRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        if (parentTransform != null)
        {
            parentTransform.rotation = originalRotation;
        }
    }
}
