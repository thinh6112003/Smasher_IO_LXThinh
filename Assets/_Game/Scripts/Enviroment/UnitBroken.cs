using DG.Tweening;
using UnityEngine;

public class UnitBroken : MonoBehaviour
{

    void Start()
    {
        Invoke(nameof(ScaleDown), Random.Range(1f, 1.5f));
        this.GetComponent<Rigidbody>()
            .AddForce(
                new Vector3(Random.Range(0f,0.5f),1.5f,Random.Range(0f,0.5f))
                , ForceMode.Impulse
            );
    }
    private void ScaleDown()
    {
        transform.DOScale(Vector3.zero, 3f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
