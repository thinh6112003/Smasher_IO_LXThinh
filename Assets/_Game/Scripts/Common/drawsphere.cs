using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawsphere : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
