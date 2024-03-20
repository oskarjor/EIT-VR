using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public SportTask scriptTask1;
    public Buttons scriptTask2;

    public int lastTaskFinished = 0;

    public void StartNextTask()
    {
        lastTaskFinished++;
        switch (lastTaskFinished)
        {
            case 1:
                scriptTask1.enabled = false;
                scriptTask2.enabled = true;
                break;
            case 2:
                scriptTask2.enabled = false;
                break;
            default:
                break;
        }
    }
}
