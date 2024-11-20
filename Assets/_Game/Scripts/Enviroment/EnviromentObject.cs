using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class EnviromentObject : MonoBehaviour
{
    [SerializeField] private GameObject originObject;
    [SerializeField] private GameObject shatters;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private bool standable = true;
    private void Start()
    {
    }
    private void Broken()
    {
        rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        originObject.SetActive(false);
        shatters.SetActive(true);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == constr.CHARACTER)
        {
            if (!standable) rigidbody.isKinematic = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == constr.DEADZONE)
        {
            Broken();
        }
    }
}
