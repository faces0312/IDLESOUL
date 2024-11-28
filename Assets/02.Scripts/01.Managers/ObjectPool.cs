using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public string Id;
    private int Size;
    private string Path;
    private GameObject Prefab;
    private Queue<GameObject> Pool;

    public ObjectPool()
    {

    }

    public ObjectPool(string id, int size, string path)
    {
        this.Id = id;
        this.Size = size;
        this.Path = path;
        this.Prefab = Resources.Load<GameObject>(path);
        this.Pool = new Queue<GameObject>();

        MakePool();
    }

    public ObjectPool(string id, int size, GameObject prefab)
    {
        this.Id = id;
        this.Size = size;
        this.Path = string.Empty;
        this.Prefab = prefab;
        this.Pool = new Queue<GameObject>();

        MakePool();
    }

    public ObjectPool(string id, int size, string path, Transform parent)
    {
        this.Id = id;
        this.Size = size;
        this.Path = path;
        this.Prefab = Resources.Load<GameObject>(path);
        this.Pool = new Queue<GameObject>();

        MakePoolWithParent(parent);
    }

    public ObjectPool(string id, int size, GameObject prefab, Transform parent)
    {
        this.Id = id;
        this.Size = size;
        this.Path = string.Empty;
        this.Prefab = prefab;
        this.Pool = new Queue<GameObject>();

        MakePoolWithParent(parent);
    }

    //private void Awake()
    //{
    //    Pool = new Queue<GameObject>();
    //    Prefab = Resources.Load<GameObject>(Path);

    //    MakePool();
    //}

    /// <summary>
    /// 오브젝트 풀의 Queue<GameObject>를 반환
    /// </summary>
    /// <returns>Queue<GameObject></returns>
    public Queue<GameObject> GetPool()
    {
        return Pool;
    }

    /// <summary>
    /// 풀 내부의 게임 오브젝트를 반환 후 다시 큐에 추가
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject GetObject()
    {
        GameObject obj = Pool.Dequeue();
        Pool.Enqueue(obj);
        return obj;
    }

    /// <summary>
    /// 풀 내부의 게임 오브젝트를 반환, 큐에 추가하지 않음
    /// </summary>
    /// <returns>GameObject</returns>
    public GameObject Dequeue()
    {
        return Pool.Dequeue();
    }

    public void MakePool()
    {
        Pool = new Queue<GameObject>();
        if (Prefab == null)
        {
            Prefab = Resources.Load<GameObject>(Path);
        }
        for (int i = 0; i < Size; i++)
        {
            GameObject obj = MonoBehaviour.Instantiate(Prefab);
            obj.SetActive(false);
            Pool.Enqueue(obj);
        }
    }

    //추천하지 않음
   public void MakePoolWithParent(Transform parent)
    {
        Pool = new Queue<GameObject>();
        for (int i = 0; i < Size; i++)
        {
            GameObject obj = MonoBehaviour.Instantiate(Prefab, parent);
            obj.SetActive(false);
            Pool.Enqueue(obj);
        }
    }

    #region SpawnFromPool OverLoading
    /// <summary>
    /// 큐의 첫 번째 오브젝트를 SetActive(true) 상태로 만듦
    /// </summary>
    public void SpawnFromPool()
    {
        GameObject obj = Pool.Dequeue();
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    /// <summary>
    /// 큐의 첫 번째 오브젝트를 Vector2 pos으로 이동 후 SetActive(true) 상태로 만듦
    /// </summary>
    public void SpawnFromPool(Vector2 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }

    /// <summary>
    /// 큐의 첫 번째 오브젝트를 Vector3 pos으로 이동 후 SetActive(true) 상태로 만듦
    /// </summary>
    public void SpawnFromPool(Vector3 pos)
    {
        GameObject obj = Pool.Dequeue();
        obj.transform.position = pos;
        obj.SetActive(true);
        Pool.Enqueue(obj);
    }
    #endregion

    public void SetActiveAllTrue()
    {
        for(int i = 0; i < Size; i++)
        {
            GameObject obj = Pool.Dequeue();
            obj.SetActive(true);
            Pool.Enqueue(obj);
        }
    }

    public void SetActiveAllFalse()
    {
        for (int i = 0; i < Size; i++)
        {
            GameObject obj = Pool.Dequeue();
            obj.SetActive(false);
            Pool.Enqueue(obj);
        }
    }
}