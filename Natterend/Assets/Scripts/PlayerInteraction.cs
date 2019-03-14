using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public Transform CameraTransform;
    public LayerMask hitMask;

    float range = 2.5f;

    void Start()
    {
        
    }

    Interactable currentTarget = null;

    void Update()
    {
        UpdateTarget();

        HUDController.Instance.ShowInteractText(currentTarget != null);

        if (currentTarget != null)
        {
            print("Interactable: " + currentTarget.name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentTarget.Interact();
            }
        }
    }

    void UpdateTarget()
    {
        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit hit, range, hitMask))
        {
            currentTarget = hit.collider.GetComponent<Interactable>();
        }
        else
            currentTarget = null;
    }
}
