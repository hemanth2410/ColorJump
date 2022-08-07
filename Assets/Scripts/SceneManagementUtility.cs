using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagementUtility : MonoBehaviour
{
    //private async void Start()
    //{
    //    AsyncOperation ao = SceneManager.UnloadSceneAsync(2);
    //    while(!ao.isDone)
    //    {
    //        await System.Threading.Tasks.Task.Yield();
    //    }
    //}
    public void loadScene(int buildIndex)
    {
        loadSceneAsynchronously(buildIndex);
    }

    async void loadSceneAsynchronously(int buildIndex)
    {
        //SceneManager.LoadScene(2);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);
        while (!asyncLoad.isDone)
        {
            await System.Threading.Tasks.Task.Yield();
        }
    }
}
