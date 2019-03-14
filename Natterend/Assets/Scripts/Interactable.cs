using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;

    public void Interact()
    {
        print("OnInteract");
        OnInteract.Invoke();
    }
}