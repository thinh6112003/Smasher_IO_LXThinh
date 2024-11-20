using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript :MonoBehaviour
{
    private int numberOfEnemy = 20;
    private Map currentMap;
    private List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();
    //[SerializeField] private LoadSceneUI loadSceneUI;
    private void Awake()
    {

        sceneToLoad.Add(SceneManager.LoadSceneAsync
        (
            "Map " + "1", LoadSceneMode.Additive)
        );
        sceneToLoad.Add(SceneManager.LoadSceneAsync(constr.HOMESCENE, LoadSceneMode.Additive));
        Invoke(nameof(LoadNextLevel), 3f);
    }

    private void LoadNextLevel()
    {

        sceneToLoad.Add(SceneManager.LoadSceneAsync
        (
            "Map "+ "2", LoadSceneMode.Additive)
        );
        sceneToLoad.Add(SceneManager.LoadSceneAsync(constr.HOMESCENE, LoadSceneMode.Additive));
    }
}