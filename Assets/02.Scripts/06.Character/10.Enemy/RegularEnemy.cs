using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEnemy : Enemy
{
    public override void Update()
    {
        base.Update();
        if (target.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (target.transform.position.x - transform.position.x > 0) // �÷��̾ �����ʿ� ���� ��
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
