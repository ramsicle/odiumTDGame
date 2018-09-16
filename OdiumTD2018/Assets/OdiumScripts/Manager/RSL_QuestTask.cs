using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RSL_QuestTask {

    public enum TaskType {
        gather,
        deliver,
        kill,
        find
    }
    public TaskType taskType;
    public int taskID;
    public string taskGiver = "";
    public string taskName = "";
    [TextArea(3, 10)]
    public string taskDescription = "";
    public bool taskStarted;
    public bool taskPending = true;
    public bool taskCompleted;
    public RSL_Item itemNeeded;
    public int hungerKeepLevelRequired;
    public int thirstKeepLevelRequired;
    public int healthKeepLevelRequired;
    
}
