using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string Header = "Header";
        public string Key = "Key";
        public Interactable Interactable;

        [HideInInspector]
        public bool Completed = false;
    }

    [SerializeField]
    Task[] Tasks;

    int curTaskIndex = -1;

    public static TaskController Instance;

    private void Awake()
    {
        Instance = this;
        GetNextTask();
        UpdateHUD();
    }

    void GetNextTask()
    {
        curTaskIndex++;

        Tasks[curTaskIndex].Interactable.SetSelected(true);
    }

    void CompleteCurrentTask()
    {
        Tasks[curTaskIndex].Completed = true;
        Tasks[curTaskIndex].Interactable.SetSelected(false);

        //print("Completed task '" + Tasks[curTaskIndex].Header + "'.");
    }

    public bool CompleteTask (string key)
    {
        if (key == Tasks[curTaskIndex].Key)
        {
            CompleteCurrentTask();

            GetNextTask();

            UpdateHUD();

            if (curTaskIndex > Tasks.Length-1)
            {
                GameInitializer.Instance.LoadGameWon();
            }
            return true;
        }
        return false;
    }

    void UpdateHUD()
    {

        if (curTaskIndex < Tasks.Length)
        {
            HUDController.Instance.SetHeader(Tasks[curTaskIndex].Header);
        }else
            HUDController.Instance.SetHeader("");
    }
}
