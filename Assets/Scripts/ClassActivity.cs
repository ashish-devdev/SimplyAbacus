using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ClassActivity : ScriptableObject
{
    public ClassData classData;
}

[System.Serializable]
public class ClassData
{

    public string nameOfClass;
    public List<ActivityList> activityList = new List<ActivityList>();

}

[System.Serializable]
public class ActivityList
{
    public string activityName;
    public string dummyActivityName;
    public MatchValueWithImage[] matchValueWithImage;
    public MatchShapeWithNumbers[] matchShapeWithNumbers;
    public AbacusOperation abacusOperations;
    public ColoringPageImages[] coloringPageImages;
    public bool speedWriting;
    public bool liftBeed01;
    public bool liftBeed02;
    public LiftBeeds liftBeeds;
    public Maze maze;
    public VisualHands visualHands;
    public CountBodyParts countBodyParts;
    public MixedMathematicalOperations mixedMathematicalOperations;
    public MultiplicationDivisionPuzzle multiplicationDivisionPuzzle;
    public MutliplicationOperation mutliplicationOperation;
    public DivisionOperation divisionOperation;
    public TutotialVideo tutorialVideo;
    public AnimatingCountingTutorial animatingCountingTutorial;
}

[System.Serializable]
public class MatchValueWithImage
{
    public Sprite img;
    public int val;
    public bool completed;
}


public class LiftBeed01
{

}

[System.Serializable]
public class ColoringPageImages
{
    public int index;
    public bool completed;
    public List<bool> numberOfImages;
}

[System.Serializable]
public class MatchShapeWithNumbers
{
    public Sprite img;
    public List<int> val;
    public Color color;
    public int correctValue;
    public bool[] completed;
}

[System.Serializable]
public class AbacusOperation
{
    public bool active;
    public TextAsset jsonData;
    public bool showFriendTable;
    public bool showMultiplicationTable;
}
[System.Serializable]
public class Maze
{
    public bool active;
    public int row;
    public int col;
    public int beginingSponingCol;
    public int beginingSponingrow;
    public int endingSponingcol;
    public int endingSponingRow;
    public Sprite floor;
    public Sprite goalImage;
    public Sprite startingImage;
    public string sideNoteMessage;




}

[System.Serializable]
public class VisualHands
{
    public bool active;
    public bool showHand1;
    public bool showHand2;
    public bool showFriendFamilyTable;
    public TextAsset abacusOperations;

}

[System.Serializable]
public class CountBodyParts
{
    public bool active;
    public Sprite animalImage;
    public string animalName;
    public List<BodyPartAndCountOfOne> bodyPartAndCountOfOne;
    public List<int> countOfAnimals;
    public bool showMultiplicationTable;
}

[System.Serializable]
public class BodyPartAndCountOfOne
{
    public string bodyPart;
    public int count;
}

[System.Serializable]
public class MixedMathematicalOperations
{
    public bool active;
    public TextAsset jsonData;
    public bool showMultiplicationTable;
    public bool showFriendTable;
}

[System.Serializable]
public class MultiplicationDivisionPuzzle
{
    public bool active;
    public TextAsset jsonData;
    public bool showMultiplicationTable;
    public bool showFriendTable;

}

[System.Serializable]
public class MutliplicationOperation
{

    public bool active;
    public TextAsset jsonData;
    public bool showMutilplicationTable;
    public bool showFriendTable;

}

[System.Serializable]
public class LiftBeeds
{
    public bool active;
    public int[] numbers;

}

[System.Serializable]
public class DivisionOperation
{
    public bool active;
    public TextAsset jsonData;
    public bool showMutilplicationTable;
    public bool showFriendTable;

}

[System.Serializable]
public class TutotialVideo
{
    public bool active;
    public string URL;
    public float time;
}

[System.Serializable]
public class AnimatingCountingTutorial
{
    public bool active;
    public float[] numbes;
    public int numberOfTimesToRepeate;
}
