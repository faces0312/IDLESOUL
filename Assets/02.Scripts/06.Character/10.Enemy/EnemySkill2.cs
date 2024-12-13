using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill2 : EnemySkillBase
{
    public GameObject bulletInstances;
    private float skillZoneTime = 2f;

    public EnemySkill2(BossEnemy bossEnemy, EnemyStateMachine stateMachine) : base(bossEnemy, stateMachine) { }

    public override IEnumerator PerformSkill()
    {
        bossEnemy.skillChargingEffect.SetActive(true);
        Debug.Log("스킬2시작");

        foreach (Transform child in bossEnemy.skillZone.transform)
        {
            child.position = new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.position.y - 0.5f, GameManager.Instance.player.transform.position.z);
            child.gameObject.SetActive(true);
        }

        yield return null;
    }

    public void SkillAttack2()
    {
        bulletInstances = EnemyManager.Instance.EnemyAttackSpawn(6007, new Vector3(bossEnemy.skillZone.transform.GetChild(0).position.x, bossEnemy.skillZone.transform.GetChild(0).position.y + 8f, bossEnemy.skillZone.transform.GetChild(0).position.z), Quaternion.Euler(90, 0, 0));

    }
}
