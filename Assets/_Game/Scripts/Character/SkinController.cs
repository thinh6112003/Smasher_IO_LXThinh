using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] private List<GameObject> listSkin = new List<GameObject>();
    [SerializeField] private bool isPlayer = false;  
    private GameObject currentSkin;
    private void Start()
    {
        Observer.AddListener(constr.CHANGESKIN, ChangeSkin);
        ChangeSkin();
    }
    public void ChangeSkin()
    {
        int idSkin = 0;
        Debug.Log("ua alo anh yeu em");
        if(isPlayer == true)
        {
            idSkin = DataRuntimeManager.Instance.dynamicData.GetIdSkin();
        }
        else
        {
            idSkin = Random.Range(0, listSkin.Count);
        }
        if (currentSkin!= null)currentSkin.SetActive(false);
        currentSkin = listSkin[idSkin];
        currentSkin.SetActive(true);
    }
}
