using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1) 
        {
            timer = 0;
            text.text = Time.deltaTime.ToString() + " ms\n " + 1f / Time.deltaTime + " fps";
        }
    }
}
