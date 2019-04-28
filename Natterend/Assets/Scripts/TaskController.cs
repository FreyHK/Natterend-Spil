using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskController : MonoBehaviour
{
    [System.Serializable]
    public class Task
    {
        public string Header = "Header";
        public string Key = "Key";
        public Interactable Interactable;
        public string Sound = "";
        public float WaitTime = 5f;

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

    public Image overlayImage;

    IEnumerator CompleteCurrentTask()
    {
        //Fade 
        Color c = overlayImage.color;
        float t = 0f;
        while (t < 1f)
        {
            c.a = t;
            overlayImage.color = c;
            t += Time.unscaledDeltaTime * 2f;
            yield return null;
        }

        Task task = Tasks[curTaskIndex];

        if (task.Sound != "")
        {
            //Play
            AudioManager.Instance.Play(task.Sound);

            //Freeze
            Time.timeScale = 0f;

            //Wait = 0f;
            while (t < task.WaitTime)
            {
                t += Time.unscaledDeltaTime;
                yield return null;
            }

            //Unfreeze
            Time.timeScale = 1f;
        }

        //Complete task
        task.Completed = true;
        task.Interactable.SetSelected(false);

        GetNextTask();
        UpdateHUD();

        //Fade
        t = 0f;
        while (t < 1f)
        {
            c.a = 1 - t;
            overlayImage.color = c;
            t += Time.unscaledDeltaTime * 2f;
            yield return null;
        }
    }


    void GetNextTask()
    {
        curTaskIndex++;

        if (curTaskIndex < Tasks.Length)
            Tasks[curTaskIndex].Interactable.SetSelected(true);
    }

    public void CompleteTask (string key)
    {
        if (key != Tasks[curTaskIndex].Key)
            return;

        if (curTaskIndex == Tasks.Length-1)
        {
            GameInitializer.Instance.LoadGameWon();
        }else
            StartCoroutine(CompleteCurrentTask());
    }

    public string GetCurrentKey()
    {
        return Tasks[curTaskIndex].Key;
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
