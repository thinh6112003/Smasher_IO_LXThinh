using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinNonSkinBase : MonoBehaviour
{
    [SerializeField] GameObject skinBase;
    private void OnEnable()
    {
        skinBase.SetActive(false);
    }
}
