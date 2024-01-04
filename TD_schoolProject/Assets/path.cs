using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public static path main;
    
    public Transform startPoint;
    public Transform[] pathPoints;
    
    private void Awake()
    {
        main = this;
    }
}
