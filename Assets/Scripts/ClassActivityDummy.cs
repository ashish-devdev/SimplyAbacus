using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ClassActivityCompletionHolder
{
    public ClassData1 classData;
}

[System.Serializable]
public class ClassActivityCompletionHolder2
{
    public ClassData2 classData;

}
[System.Serializable]
public class DailyWorkoutCompletionHolder3
{
    public List<DailyWorkOutInformation> dailyWorkOutInformation;

}

public class AppLockInformationHolder3
{
    public List<LockInformation> dailyWorkOutInformation;

}




[System.Serializable]
public class ClassData1
{
    public string nameOfClass;
    public List<ActivityList1> activityList = new List<ActivityList1>();
}
[System.Serializable]
public class ClassData2
{
    public string nameOfClass;
    public List<ActivityList2> activityList = new List<ActivityList2>();
}

[System.Serializable]
public class ClassData3
{
    public List<ActivityList3> activityList = new List<ActivityList3>();
}




[System.Serializable]
public class ActivityList1
{
    public string activityName;
    public List<MatchValueWithImage1> matchValueWithImage;
    public List<MatchShapeWithNumbers1> matchShapeWithNumbers;
    public bool coloringPageImages;
    public bool[] abacusOperations;
    public bool speedWriting;
    public bool[] liftBeed01;//= new bool[4];
    public bool[] liftBeed02;//= new bool[5];
    public LiftBeeds1 liftBeeds1;
    public Maze1 maze1;
    public VisualHands1 visualHands1;
    public CountBodyParts1 countBodyParts1;
    public MixedMathematicalOperations1 mixedMathematicalOperations1;
    public MultiplicationDivisionPuzzle1 multiplicationDivisionPuzzle1;
    public MultiplicationOperation1 multiplicationOperation1;
    public DivisionOperation1 divisionOperation1;
    public TutorialVideo1 tutorialVideo1;
    public AnimatingCountingTutorial1 animatingCountingTutorial1;
}

[System.Serializable]
public class ActivityList2
{
    public string activityName;
    public List<MatchValueWithImage2> matchValueWithImage;
    public List<MatchShapeWithNumbers2> matchShapeWithNumbers;
    public ColoringPageImages2 coloringPageImages;
    public AbacusOperations2 abacusOperations;
    public SpeedWriting2 speedWriting;
    public LiftingBeed21 liftingBeed21;
    public LiftingBeed22 LiftingBeed22;
    public LiftBeeds2 liftBeeds2;
    public Maze2 maze2;
    public VisualHands2 visualHands2;
    public CountBodyParts2 countBodyParts2;
    public MixedMathematicalOperations2 mixedMathematicalOperations2;
    public MultiplicationDivisionPuzzle2 multiplicationDivisionPuzzle2;
    public MultiplicationOperation2 multiplicationOperation2;
    public DivisionOperation2 divisionOperation2;
    public TutorialVideo2 tutorialVideo2;
    public AnimatingCountingTutorial2 animatingCountingTutorial2;


}

[System.Serializable]
public class ActivityList3
{
    public DailyWorkOutInformation dailyWorkOutInformation;

}

[System.Serializable]
public class MatchValueWithImage1
{
    public bool completed;
}


public class LiftBeed011
{
    bool completed;
}

[System.Serializable]
public class ColoringPageImages1
{
    public int index;
    public bool completed;
}


[System.Serializable]
public class MatchShapeWithNumbers1
{
    // public List<int> val;
    public int correctValue;
    public List<bool> completed;
}

[System.Serializable]
public class Maze1
{
    public bool completed;
}

[System.Serializable]

public class VisualHands1
{
    public bool[] completed;
}


[System.Serializable]
public class CountBodyParts1
{
    public bool[] completed;
}
[System.Serializable]
public class MixedMathematicalOperations1
{
    public bool[] completed;
}

[System.Serializable]
public class MultiplicationOperation1
{
    public bool[] completed;
}

[System.Serializable]
public class MultiplicationDivisionPuzzle1
{
    public bool[] completed;
}

[System.Serializable]
public class DivisionOperation1
{
    public bool[] completed;
}

[System.Serializable]
public class LiftBeeds1
{
    public bool completed;
}
[System.Serializable]
public class TutorialVideo1
{
    public bool completed;
}

[System.Serializable]
public class AnimatingCountingTutorial1
{
    public bool[] completed;
}


[System.Serializable]
public class MatchValueWithImage2
{
    public float bestTime = -1;
    public float currentSavedTime = 0;
    public string bestTime_string = "-";
}
[System.Serializable]

public class MatchShapeWithNumbers2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}
[System.Serializable]

public class ColoringPageImages2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class AbacusOperations2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}
[System.Serializable]
public class SpeedWriting2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class LiftingBeed21
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
    public int currentSubActivity = 0;
}

[System.Serializable]
public class LiftingBeed22
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
    public int currentSubActivity = 0;
}
[System.Serializable]
public class Maze2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class VisualHands2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class CountBodyParts2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class MixedMathematicalOperations2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class MultiplicationDivisionPuzzle2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}
[System.Serializable]
public class MultiplicationOperation2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}
[System.Serializable]
public class LiftBeeds2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class DivisionOperation2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}

[System.Serializable]
public class TutorialVideo2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;
}


[System.Serializable]
public class AnimatingCountingTutorial2
{
    public float bestTime = -1;
    public string bestTime_string = "-";
    public float currentSavedTime = 0;

}



[System.Serializable]
public class DailyWorkOutInformation
{
    public int id;
    public string QuestionType;
    public MODE mode;
    public int totalQuestions;
    public int totalCorrectAnswers;
    public float totalTime;
}

[System.Serializable]
public class LockInformation
{
    public int id;
    public bool isLock;

}


[System.Serializable]
public enum MODE
{
    easy, medium, hard
}