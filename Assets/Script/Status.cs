using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public bool task1 = false;
    public bool task2 = false;
    public bool task3 = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(task2){
            //enable script for task 3
        }
        if(task1){
            //enable script for task 2
        }
    }
}
