using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tab : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;
    Color tmpColor= new Color();

    public void OnEnable()
    {
        RevertColor();
    }
    public void OnDisable()
    {
        RevertColor();
    }
    public void RevertColor()
    {
        Color tmpColor = buttonImage.color;
        buttonImage.color = buttonText.color;
        buttonText.color = tmpColor;
    }
}
