using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicAttack : MonoBehaviour
{
    public LayerMask TargetLayer; //해당 투사체를 맞추기 위한 타겟 레이어
    public ParticleSystem mainParticle;
    public Collider collider;
    public GameObject skillZone;

    private void OnEnable()
    {
        collider.enabled = false;
        skillZone.SetActive(true);
        mainParticle.gameObject.SetActive(false);
        StartCoroutine("DisEnable");
    }

    IEnumerator DisEnable()
    {
        yield return new WaitForSeconds(1.5f);
        skillZone.SetActive(false);
        mainParticle.gameObject.SetActive(true);
        collider.enabled = true;
        mainParticle.Play();
        StartCoroutine(WaitForParticleSystemToFinish());
    }

    IEnumerator WaitForParticleSystemToFinish()
    {
        while (mainParticle.IsAlive(true))
        {
            yield return null;
        }
        collider.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TargetLayer == ((1 << other.gameObject.layer) | TargetLayer))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ApplyStun();
            }
        }
    }
}
