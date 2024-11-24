using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tab : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;
    Color tmpColor;

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
        tmpColor = buttonImage.color;
        buttonImage.color = buttonText.color;
        buttonText.color = tmpColor;
    }
}
