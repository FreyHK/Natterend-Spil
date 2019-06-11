using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class belongs to persistent scene
/// </summary>
public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance;

    public Image overlayImage;

    Scene loadedScene;

    LoadSceneParameters loadParameters = new LoadSceneParameters(LoadSceneMode.Additive);

    private void Start()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;

        //Hide overlayimage
        Color c = overlayImage.color;
        c.a = 0f;
        overlayImage.color = c;

        if (SceneManager.sceneCount == 1)
        {
            loadedScene = SceneManager.LoadScene(1, loadParameters);
        }
        else
        {
            //Find the level, that we loaded in from editor.
            loadedScene = SceneManager.GetSceneAt(1);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Remember loaded scene (used when unloading)
        SceneManager.SetActiveScene(scene);
    }

    public void LoadMenu()
    {
        LoadScene(1);
    }

    public void LoadGame()
    {
        LoadScene(2);
    }

    public void LoadGameWon()
    {
        LoadScene(3);
    }

    public void LoadGameOver()
    {
        LoadScene(4);
    }

    void LoadScene(int buildIndex)
    {
        //Don't load if we are already
        if (IsLoading)
            return;

        //Load with fading animations
        StartCoroutine(LoadSceneAsync(buildIndex));
    }

    public bool IsLoading { get; private set; }

    IEnumerator LoadSceneAsync(int buildIndex)
    {
        IsLoading = true;

        //Fade 
        Color c = overlayImage.color;
        float t = 0f;
        while (t < 1f)
        {
            c.a = t;
            overlayImage.color = c;
            t += Time.deltaTime * 2f;
            yield return null;
        }
        c.a = 1f;
        overlayImage.color = c;

        //Unload old scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(loadedScene);
        //Load new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex, loadParameters);

        //Wait for both to finish
        while (!asyncLoad.isDone || !asyncUnload.isDone)
        {
            yield return null;
        }
        loadedScene = SceneManager.GetSceneAt(1);

        //Reset flag
        IsLoading = false;

        //Fade
        t = 0f;
        while (t < 1f)
        {
            c.a = 1 - t;
            overlayImage.color = c;
            t += Time.deltaTime * 2f;
            yield return null;
        }
        c.a = 0;
        overlayImage.color = c;
    }
}
