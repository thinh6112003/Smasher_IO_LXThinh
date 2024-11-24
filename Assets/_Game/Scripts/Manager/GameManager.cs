using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private MapSO mapSO;
    private int numberOfEnemy= 20;
    private Map currentMap;
    private Player player;
    private Vector3 beginPos;
    private List<AsyncOperation> sceneToLoad = new List<AsyncOperation>();
    //[SerializeField] private LoadSceneUI loadSceneUI;
    private void Awake()
    {
        //DOTween.Init();
        DontDestroyOnLoad(gameObject);
        Observer.AddListener(constr.ONEMOREKILL, UpdateNumberOfEnemy);
    }

    private void UpdateNumberOfEnemy()
    {
        numberOfEnemy--;
        if (numberOfEnemy == 0) 
        {
            Observer.Noti(constr.WINGAME);
        }
    }

    private void Start()
    {
        InitGame();
    }
    private void Update()
    {
        Debug.Log(Time.deltaTime+ " ms , "+ 1f/Time.deltaTime+ " fps");
    }
    private void InitGame()
    {
        Observer.AddListener(constr.NEXTLEVEL, LoadNextLevel);
        Observer.AddListener(constr.RELOADLEVEL, ReLoadLevel);
        sceneToLoad.Add(SceneManager.LoadSceneAsync
        (
            DataRuntimeManager.Instance.dynamicData.CurrentEnviromentSceneName(), LoadSceneMode.Additive)
        );
        sceneToLoad.Add(SceneManager.LoadSceneAsync(constr.HOMESCENE, LoadSceneMode.Additive));
        StartCoroutine(LoadingProgress());
    }
    private IEnumerator LoadingProgress()
    {
        while (!sceneToLoad[0].isDone || !sceneToLoad[1].isDone) yield return null;
        int idLevel = DataRuntimeManager.Instance.dynamicData.currentIDLevel;
        Scene targetScene = SceneManager.GetSceneByName("Map " + idLevel);
        SceneManager.SetActiveScene(targetScene);
        EnemyManager.Instance.InitEnemy(numberOfEnemy);
        Observer.Noti(constr.DONELOADSCENEASYNC);
        currentMap = Instantiate(mapSO.GetMapByID(idLevel));
        Invoke(nameof(UnloadScene), 1f);
    }
    private void UnloadScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(constr.LOADSCENE));
    }
    private void LoadNextLevel()
    {
        numberOfEnemy = 20;
        StartCoroutine(LoadingLevel(DataRuntimeManager.Instance.dynamicData.GetCurrentIDLevel()));
    }

    private void ReLoadLevel()
    {
        numberOfEnemy = 20;
        StartCoroutine(LoadingLevel(DataRuntimeManager.Instance.dynamicData.GetCurrentIDLevel(), true));
    }
    private IEnumerator LoadingLevel(int idLevel, bool reload = false)
    {
        yield return new WaitForSeconds(0.3f);
        Scene sceneUnLoad = SceneManager.GetSceneByName("Map " + (reload ? idLevel : idLevel == 1 ? 5 : idLevel - 1).ToString());
        Destroy(currentMap);
        SceneManager.UnloadSceneAsync(sceneUnLoad);

        yield return new WaitForSeconds(0.3f);
        AsyncOperation t = SceneManager.LoadSceneAsync(
            DataRuntimeManager.Instance.dynamicData.CurrentEnviromentSceneName(), LoadSceneMode.Additive
        );

        while (!t.isDone) yield return null;
        Scene targetScene = SceneManager.GetSceneByName("Map " + idLevel.ToString());
        SceneManager.SetActiveScene(targetScene);
        currentMap = Instantiate(mapSO.GetMapByID(idLevel));
        UIManager.Instance.SetUIScene(UIManager.SceneUIType.Home);
        EnemyManager.Instance.InitEnemy(numberOfEnemy);
        Observer.Noti(constr.DONELOADLEVEL);
    }
    public void setPlayer(Player player)
    {
        this.player = player;
    }
}