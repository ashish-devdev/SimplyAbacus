using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01;

public class ClassManagerWrapper : MonoBehaviour
{
    public ClassManager classManager { get { return FindObjectOfType<ClassManager>(); } }
}

public class ClassManager : MonoBehaviour
{
    public List<ClassActivity> classActivities = new List<ClassActivity>();
    public Example01 classes;
    public static string currentClassName;
    public static string currentActivityName;
    public static int currentActivityIndex;
    public ClassActivity currentClassActivity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
