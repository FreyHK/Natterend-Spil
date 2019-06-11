using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public bool Conditional = true;
    public string Key = "check";

    public UnityEvent OnInteract;

    public Material OnSelectedMaterial;

    public MeshRenderer meshRenderer;
    Material[] mats;

    public void SetSelected(bool s)
    {
        //print("Set selected: " + s.ToString() + ", " + transform.parent.name);
        if (s)
        {
            //Remember original materials
            mats = meshRenderer.materials;
            //Set new
            meshRenderer.material = OnSelectedMaterial;
        }
        else
        {
            //Set back to original
            meshRenderer.materials = mats;
        }
    }

    public void Interact()
    {
        if (CanInteract())
        {
            TaskController.Instance.CompleteTask(Key);
            OnInteract.Invoke();
        }
    }

    public bool CanInteract()
    {
        return TaskController.Instance.GetCurrentKey() == Key || !Conditional;
    }
}