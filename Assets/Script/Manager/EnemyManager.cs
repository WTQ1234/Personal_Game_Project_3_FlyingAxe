using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HRL;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public List<Enemy> EnemiesList;
    public Transform startTransform;
    public float radious = 10;

    public float time;
    private float _time;

    void Start()
    {
        _time = 3;
        for(int i = 0; i < EnemiesList.Count; i++)
        {
            ObjectPoolManager.Instance.InitObjectPool(EnemiesList[i].gameObject);
        }
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time > time)
        {
            _time = 0;
            Check();
            Check();
        }
    }

    void Check()
    {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        if (dir.x == 0f)
        {
            dir.x = 0.1f;
        }
        if (dir.y == 0f)
        {
            dir.y = 0.1f;
        }
        dir.Normalize();
        Vector3 pos = startTransform.position + dir * radious;
        Enemy p = EnemiesList[Random.Range(0, EnemiesList.Count)];
        GameObject enemy = ObjectPoolManager.Instance.GetObject(p.gameObject);
        enemy.SetActive(true);
        enemy.transform.position = pos;
        enemy.transform.rotation = Quaternion.identity;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startTransform.position, radious);
    }
}
