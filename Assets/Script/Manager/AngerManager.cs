using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class AngerManager : MonoBehaviour
{
    public int anger = 0;

    public int curAngerState = 0;

    public List<int> AngerState = new List<int>();

    private void Awake()
    {
        anger = 0;
        curAngerState = 0;
    }

    public void AddAnger(int _anger)
    {
        anger += _anger;
        _CheckAnger_Up();
    }

    private void _CheckAnger_Up()
    {
        int next_state = curAngerState + 1;
        if (AngerState.Count > curAngerState)
        {
            // 怒气满了
            return;
        }
        else
        {
            int next_anger_target = AngerState[next_state];
            if (anger > next_anger_target)
            {
                // 下一怒气阶段
                curAngerState++;
                return;
            }
        }
    }

}
