using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinWithSkinBase : MonoBehaviour
{
    [SerializeField] GameObject skinBase;
    private void OnEnable()
    {
        skinBase.SetActive(true);
    }
}
