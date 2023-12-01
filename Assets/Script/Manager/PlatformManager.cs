using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlatformManager : MonoBehaviour
{
    [ShowInInspector]
    public Dictionary<int, int> platformPairs = new Dictionary<int, int>(); 
    public List<Platform> Platforms;
    public Transform startTransform;
    public float width;

    public float time;
    public float minSubTime;
    private float _totalTime;
    private float _time;

    void Start()
    {
        _time = 0;
    }

    void Update()
    {
        _time += Time.deltaTime;
        _totalTime += Time.deltaTime;
        if (_time > time)
        {
            _time = 0;
            Check();
        }
    }

    void Check()
    {
        Platform p = Platforms[Random.Range(0, Platforms.Count)];
        int curWidth = p.width;
        for (int i = 0; i < 5; i++)
        {
            bool canCreate = true;
            Vector3 randomPos = new Vector3(Random.Range(-width, width), 0, 0);
            int curX = Mathf.FloorToInt(randomPos.x);
            for (int j = -curWidth / 2; j < curWidth / 2; j++)
            {
                var cur_x_by_width = curX + j;
                if (platformPairs.ContainsKey(cur_x_by_width))
                {
                    var sub_time = platformPairs[cur_x_by_width];
                    if (_totalTime - sub_time < minSubTime)
                    {
                        canCreate = false;
                        break;
                    }
                }
            }
            if (!canCreate)
            {
                continue;
            }
            for (int j = -curWidth / 2; j < curWidth / 2; j++)
            {
                var cur_x_by_width = curX + j;
                platformPairs[cur_x_by_width] = Mathf.FloorToInt(_totalTime);
            }
            Vector3 pos = startTransform.position + randomPos;
            Instantiate(p, pos, Quaternion.identity, startTransform);
            break;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startTransform.position + Vector3.right * width, startTransform.position + Vector3.left * width);
    }
}
