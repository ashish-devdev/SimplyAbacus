using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = " New Maze Inputs", menuName = "Maze Inputs", order = 2)]
[System.Serializable]
public class MazeScriptableInputs : ScriptableObject
{
    public MazeData[] mazeDatas;
}

[System.Serializable]
public class MazeData
{
    public Sprite startSprite;
    public Sprite endSprite;
    public Sprite background;
    public Color trailColor;
    public string noteMessage;
    public Color wallColor;
}