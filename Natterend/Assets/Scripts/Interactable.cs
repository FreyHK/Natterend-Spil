using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public bool Conditional = true;
    public string Key = "check";

    public UnityEvent OnInteract;

    public Material OnSelectedMaterial;

    public MeshRenderer renderer;
    Material[] mats;

    public void SetSelected(bool s)
    {
        print("Set selected: " + s.ToString() + ", " + transform.parent.name);
        if (s)
        {
            //Remember original materials
            mats = renderer.materials;

            //Set new
            renderer.material = OnSelectedMaterial;
        }
        else
        {
            //Set back to original
            renderer.materials = mats;
        }
    }

    public void Interact()
    {
        if (TaskController.Instance.CompleteTask(Key) || !Conditional)
        {
            OnInteract.Invoke();
        }
    }
}