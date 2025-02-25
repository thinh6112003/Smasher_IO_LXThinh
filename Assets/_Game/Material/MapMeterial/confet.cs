using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class confet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Observer.AddListener(constr.WINGAME, play);
    }

    // Update is called once per frame
    async void play()
    {
        await Task.Delay(1000);
        GetComponent<ParticleSystem>().Play();
    }
}
