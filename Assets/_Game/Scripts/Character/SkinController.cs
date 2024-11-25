using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] private List<GameObject> listSkin = new List<GameObject>();
    private GameObject currentSkin;
    private void Awake()
    {
        Observer.AddListener(constr.CHANGESKIN, ChangeSkin);
    }
    private void ChangeSkin()
    {
        Debug.Log("changeSkin");
        int idSkin = DataRuntimeManager.Instance.dynamicData.GetIdSkin();
        if(currentSkin!= null)currentSkin.SetActive(false);
        currentSkin = listSkin[idSkin];
        currentSkin.SetActive(true);
    }
}
