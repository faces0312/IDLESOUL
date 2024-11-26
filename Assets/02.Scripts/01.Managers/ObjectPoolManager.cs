using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private Dictionary<string, List<ObjectPool>> poolDict;

    public List<ObjectPool> GetPool(string id)
    {
        return poolDict[id];
    }

    public void AddPool(string id, ObjectPool pool)
    {
        if (poolDict[id] == null)
        {
            List<ObjectPool> newPool = new List<ObjectPool>();
            newPool.Add(pool);

            poolDict.Add(id, newPool);
        }
        else if (poolDict[id] != null)
        {
            poolDict[id].Add(pool);
        }
    }

}

[System.Serializable]
public class ObjectPool : MonoBehaviour
{
    public string Id;
    public int Size;
    public string Path;
    public GameObject Prefab;
    public Queue<GameObject> Pool;

    private void Awake()
    {
        Pool = new Queue<GameObject>();
    }

    public Queue<GameObject> GetPool()
    {
        return Pool;
    }

    public void MakePool()
    {
        Pool = new Queue<GameObject>();

        for (int i = 0; i < Size; i++)
        {
            GameObject obj = Instantiate(Prefab);
            obj.SetActive(false);
            Pool.Enqueue(obj);
        }
    }

    public void SpawnFromPool()
    {
        GameObject obj = Pool.Dequeue();
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    public void SpawnFromPool(Vector2 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    public void SpawnFromPool(Vector3 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }
}