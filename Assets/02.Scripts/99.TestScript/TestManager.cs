using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : SingletonDDOL<TestManager>
{
    // 여러 기능들을 테스트 하기 위한 목적의 매니저 클래스
    // 형식을 신경쓰지 않고, 자유롭게 사용

    public GameObject TestPlayer;
    public Soul TestSoul;
    
    public void OnSpawnEnemy()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Enemy/TestEnemy");

        GameObject testEnemy = Instantiate(prefab, Vector2.right * 7.5f, Quaternion.identity);
        GameObject testEnemy2 = Instantiate(prefab, Vector2.left * 5f, Quaternion.identity);
        GameObject testEnemy3 = Instantiate(prefab, Vector2.up * 2f, Quaternion.identity);
        GameObject testEnemy4 = Instantiate(prefab, Vector2.down * 3.5f, Quaternion.identity);

        GameManager.Instance.enemies.Add(testEnemy);
        GameManager.Instance.enemies.Add(testEnemy2);
        GameManager.Instance.enemies.Add(testEnemy3);
        GameManager.Instance.enemies.Add(testEnemy4);
    }

    public void OnUseDefaultSkill()
    {
        TestSoul.UseSkill(TestSoul.Skills[(int)SkillType.Default]);
    }

    public void OnUseUltimateSkill()
    {
        TestSoul.UseSkill(TestSoul.Skills[(int)SkillType.Ultimate]);
    }
}
