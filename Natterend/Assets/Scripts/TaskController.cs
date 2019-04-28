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

        if (curTaskIndex < Tasks.Length)
            Tasks[curTaskIndex].Interactable.SetSelected(true);
    }


    void CompleteCurrentTask()
    {
        Tasks[curTaskIndex].Completed = true;
        Tasks[curTaskIndex].Interactable.SetSelected(false);
    }


        /*
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

        //float duration = AudioManager.Instance.GetClipLength()

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
    */

    public bool CompleteTask (string key)
    {
        if (key == Tasks[curTaskIndex].Key)
        {
            CompleteCurrentTask();

            GetNextTask();

            UpdateHUD();

            if (curTaskIndex == Tasks.Length)
            {
                GameInitializer.Instance.LoadGameWon();
            }
            return true;
        }
        return false;
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
