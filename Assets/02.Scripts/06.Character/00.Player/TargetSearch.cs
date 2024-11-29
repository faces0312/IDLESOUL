using UnityEngine;

//전투 중에는 <일점 벙위 안에 적을 찾는 기능>이 켜지면 안됨
//이동중에만 동작해야함 
public class TargetSearch : MonoBehaviour
{
    [SerializeField] private float radius; //적 타겟 탐색 거리 
    [SerializeField] private GameObject shortEnemyTarget;
    [SerializeField] private LayerMask layer; //타겟으로 설정할 레이어 설정
    private Collider[] colliders;

    public float Radius { get => radius; set => radius = value; }
    public GameObject ShortEnemyTarget { get => shortEnemyTarget; }

    public void OnTargetSearch()
    {
        if (shortEnemyTarget == null)
        {
            colliders = Physics.OverlapSphere(transform.position, radius, layer);

            if (colliders.Length > 0)
            {
                float short_distance = Vector3.Distance(transform.position, colliders[0].transform.position);
                foreach (Collider col in colliders)
                {
                    float short_distance2 = Vector3.Distance(transform.position, col.transform.position);
                    if (short_distance >= short_distance2)
                    {
                        short_distance = short_distance2;
                        shortEnemyTarget = col.gameObject;
                    }
                }
                Debug.Log($"가까운 Enemy 타겟 : {shortEnemyTarget.name} ");
                return;
            }
        }
    }

    public void TargetClear()
    {
        shortEnemyTarget = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}