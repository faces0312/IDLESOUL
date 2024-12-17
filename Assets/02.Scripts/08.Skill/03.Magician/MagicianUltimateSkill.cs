using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianUltimateSkill : Skill
{
    GameObject skillPrefab;
    private float range;
    private float totalValue;

    public MagicianUltimateSkill(int id) : base(id)
    {
        // TODO : DB 에서 받아 넣기
        coolTime = 5f;
        skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Meteor");
        range = 10f;
        totalValue = value * (level * upgradeValue);
    }

    public override void UpgradeSkill(int amount)
    {
        level += amount;

        // TODO : 배율 조정
        totalValue = value * (level * upgradeValue);
    }

    public override void UseSkill(StatHandler statHandler)
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;

        playerPos += skillPrefab.transform.position;

        GameObject meteor = Object.Instantiate(skillPrefab, playerPos, Quaternion.LookRotation(skillPrefab.transform.forward));

        if (GameManager.Instance.player.PlayerAnimationController.skeleton.ScaleX > 0)
        {
            meteor.transform.position -= new Vector3(skillPrefab.transform.position.x * 2f, 0, 0);
            meteor.transform.Rotate(new Vector3(90f, 0, 0));
        }

        if (meteor.TryGetComponent(out Meteor component))
        {
            component.InitSettings(statHandler.CurrentStat.atk * (int)totalValue, range);
        }

        GameManager.Instance.cameraController.MeteorEffect();
    }
}
