using UnityEngine;

public class EnviromentObject : MonoBehaviour
{
    [SerializeField] private GameObject originObject;
    [SerializeField] private GameObject shatters;
    [SerializeField] private Rigidbody myRigidbody;
    [SerializeField] private bool standable = true;
    private void Start()
    {
    }
    private void Broken()
    {
        myRigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        originObject.SetActive(false);
        shatters.SetActive(true);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == constr.CHARACTER)
        {
            if (!standable) myRigidbody.isKinematic = false;
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
