using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class MageUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float totalValue;

    public MageUltimateSkill(int id) : base(id)
    {
        // TODO : DB 에서 받아 넣기
        coolTime = 30f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/MultipleStorm");
        range = 7f;
        totalValue = level * upgradeValue;
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        totalValue = level * upgradeValue;
    }

    public override void UseSkill(StatHandler statHandler)
    {
        CoroutineHelper.Instance.StartCoroutineHelper(CoroutineSkillStart(statHandler));
    }

    private IEnumerator CoroutineSkillStart(StatHandler statHandler)
    {
        while (GameManager.Instance.isCutScene)
        {
            yield return null;
        }

        Vector3 playerPos = new Vector3(GameManager.Instance.player.transform.position.x, 0, GameManager.Instance.player.transform.position.z);

        playerPos += skillPrefab.transform.position;

        GameObject multipleStorm = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));

        if (multipleStorm.TryGetComponent(out MultipleStorm component))
        {
            component.InitSettings((BigInteger.Divide(statHandler.CurrentStat.atk, 10) + (int)totalValue) * (int)value, range);
        }

        //GameManager.Instance.cameraController.MeteorEffect();
    }
}
