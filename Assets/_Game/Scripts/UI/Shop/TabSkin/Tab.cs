using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Tab : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;
    Color textColor;
    Color imageColor;

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
        imageColor = buttonImage.color;
        textColor = buttonText.color;
        buttonImage.DOColor(textColor, 0.22f);
        buttonText.DOColor(imageColor, 0.22f);
    }
}
