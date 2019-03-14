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

        [HideInInspector]
        public bool Completed = false;
    }

    [SerializeField]
    Task[] Tasks;

    int curTaskIndex = 0;


    private void Start()
    {
        UpdateHUD();
    }

    public void CompleteTask (string key)
    {

        print("Key: " + key + ", TargetKey: " + Tasks[curTaskIndex].Key);

        if (key == Tasks[curTaskIndex].Key)
        {
            print("Completed task '" + Tasks[curTaskIndex].Header + "'.");

            Tasks[curTaskIndex].Completed = true;
            curTaskIndex++;

            UpdateHUD();

            if (curTaskIndex > Tasks.Length-1)
            {
                print("Completed all tasks.");
            }
        }
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
