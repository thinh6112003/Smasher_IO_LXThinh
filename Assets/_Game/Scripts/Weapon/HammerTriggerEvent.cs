using UnityEngine;

public class HammerTriggerEvent : MonoBehaviour
{
    public Animator animHammer;
    public string currentHammerAnimName = constr.IDLE;
    public GameObject myParticleSystem;
    public GameObject character;
    public string colliderHashCode= "";
    bool isrun = false;
    private void Start()
    {
        colliderHashCode = character.GetHashCode().ToString();
    }
    public void TriggerEvent()
    {
        SetAttackHammer();
        Observer.AddListener(constr.RUN+ colliderHashCode, CancelAttack);
    }
    public void EndAttack()
    {
        Observer.Noti(constr.IDLE+ colliderHashCode);
    }
    public void SetAttackHammer()
    {
        if (isrun) return;
        Observer.Noti(constr.ATTACK+ colliderHashCode);
        ParticleSystem[] particles=  myParticleSystem.GetComponentsInChildren<ParticleSystem>();
        for(int i=0;i < particles.Length; i++)
        {
            particles[i].Play();
        }
        animHammer.ResetTrigger(currentHammerAnimName);
        currentHammerAnimName = constr.ATTACK;
        animHammer.SetTrigger(currentHammerAnimName);
        currentHammerAnimName = constr.IDLE;
    }
    public void CancelAttack()
    {
        animHammer.ResetTrigger(constr.ATTACK);
        animHammer.SetTrigger(constr.IDLE);

        Observer.RemoveListener(constr.RUN + colliderHashCode, CancelAttack);
    }
}
