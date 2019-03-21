using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public bool Conditional = true;
    public string Key = "check";

    public UnityEvent OnInteract;

    public void Interact()
    {
        if (TaskController.Instance.CompleteTask(Key) || !Conditional)
        {
            OnInteract.Invoke();
        }
    }
}