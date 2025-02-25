using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _GameObject;
    void Start()
    {
        // Gọi hàm RepeatMethod mỗi 3 giây, bắt đầu sau 3 giây
        InvokeRepeating("RepeatMethod", 3.0f, 3.0f);
    }

    void RepeatMethod()
    {
        Instantiate(_GameObject, new Vector3(3, 0, 3), Quaternion.identity);
        Debug.Log("RepeatMethod called");
    }
}
