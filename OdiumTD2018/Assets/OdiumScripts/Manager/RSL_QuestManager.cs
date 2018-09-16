using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// task ID's explained
/// example = 101, the 1 = (NPC)Chris, 01 represents the order of task given from 01 t0 99...
/// example = 201, the 2 = (NPC)Allan, and same as above...
/// 
/// </summary>
public class RSL_QuestManager : MonoBehaviour {

    public RSL_QuestTask[] questTask;

    public GameObject[] questPanels;

    public Text questDescriptionTxt;

    public int hungerKeepLevel = 0;
    public int thirstKeepLevel = 0;
    public int healthKeepLevel = 0;
    public int defenseLevel = 0;
    public int offenseLevel = 0;
    public int comfortLevel = 0;

    public bool[] taskCompleted;

    public RSL_SceneUIManager sceneManager;
    public RSL_LoadSaveManager lsManager;

    private string taskGiver = "";
    private string taskName = "";
    private string taskDescription = "";
    private int givenTask;

    private void Start() {
        for (int i=0; i<questPanels.Length; i++) {
            questPanels[i].SetActive(false);
        }

        taskCompleted = new bool[questTask.Length];
        for(int i=0; i<lsManager.scene01Objects.Length; i++) {
            if (lsManager.scene01Objects[i].CompareTag("SceneManager")) {
                sceneManager = lsManager.scene01Objects[i].GetComponent<RSL_SceneUIManager>();
            }
        }
    }

    public void GiveTask(int ID) {
        givenTask = ID;
        foreach (RSL_QuestTask task in questTask) {
            if (task.taskID == ID) {
                task.taskStarted = true;
                taskGiver = task.taskGiver;
                taskName = task.taskName;
                taskDescription = task.taskDescription;
            }
        }

        UpdateQuestUI(ID, true, taskGiver, taskName);
    }

    public void TaskBtnPressed(int ID) {
        for (int i = 0; i < questTask.Length; i++) {
            if (questTask[i].taskID == ID) {
                questDescriptionTxt.text = questTask[i].taskDescription;
            }
        }
    }

    private void  UpdateQuestUI(int id, bool started, string strName, string strTask) {
        //Debug.Log("TaskID: " + givenTask + " given by: " + taskGiver);
        for(int i=0; i<questTask.Length; i++) {
            if (questTask[i].taskID == id) {
                questPanels[i].SetActive(true);
                questPanels[i].GetComponentInChildren<Image>().color = Color.blue;
                questPanels[i].GetComponentInChildren<Text>().text = strName;
                questPanels[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = strTask;
            }
        }
    }

    /// Items:          hungerKeepLevel:    thirstKeepLevel:    healthKeepLevel:    
    /// Fruit -              +2                    +1                 +1            
    /// Can food -           +5                    +2                 +2
    /// FirstAid -            0                     0                 +10
    /// Stimpak -            +10                   +10                +15
    /// Drugs -              +5                    +0                 +12
    /// 
    /// Category's      Defense:    Offense:
    /// Weapon -            +10         +20
    /// Ammo -              +5          +10
    /// ...
    public void AddItemToList(string itemName, int itemQuantity) {
        switch(itemName) {
            case "fruit":
                hungerKeepLevel += (itemQuantity * 2);
                thirstKeepLevel += (itemQuantity * 1);
                healthKeepLevel += (itemQuantity * 1);
                break;
            case "can food":
                hungerKeepLevel += (itemQuantity * 5);
                thirstKeepLevel += (itemQuantity * 2);
                healthKeepLevel += (itemQuantity * 2);
                break;
            case "FirstAid":
                hungerKeepLevel += (itemQuantity * 0);
                thirstKeepLevel += (itemQuantity * 0);
                healthKeepLevel += (itemQuantity * 10);
                break;
            case "Stimpak":
                hungerKeepLevel += (itemQuantity * 10);
                thirstKeepLevel += (itemQuantity * 10);
                healthKeepLevel += (itemQuantity * 15);
                break;
            case "Drugs":
                hungerKeepLevel += (itemQuantity * 5);
                thirstKeepLevel += (itemQuantity * 0);
                healthKeepLevel += (itemQuantity * 12);
                break;
            case "Weapon":
                defenseLevel += (itemQuantity * 10);
                offenseLevel += (itemQuantity * 20);
                break;
            case "Ammo":
                defenseLevel += (itemQuantity * 5);
                offenseLevel += (itemQuantity * 10);
                break;
        }

        CheckQuestStatus();
    }

    private void CheckQuestStatus() {
        int count = 0;
        foreach(RSL_QuestTask qt in questTask) {
            if (qt.taskStarted) {
                if (qt.hungerKeepLevelRequired < hungerKeepLevel && qt.thirstKeepLevelRequired < thirstKeepLevel
                && qt.healthKeepLevelRequired < healthKeepLevel) {
                    qt.taskCompleted = true;
                    qt.taskPending = false;
                    taskCompleted[count] = true;
                    ConfirmScene01TaskCompleted(qt.taskID);
                }
            }
            count++;
        }
    }

    public void ConfirmScene01TaskCompleted(int taskID) {
        switch(taskID) {
            case 101:
                sceneManager.Task01 = true;
                sceneManager.QuestSetNode(1);
                break;
            case 102:
                sceneManager.Task01 = true;
                sceneManager.QuestSetNode(2);
                break;
            case 103:
                sceneManager.Task01 = true;
                sceneManager.QuestSetNode(3);
                break;
            case 104:
                sceneManager.Task01 = true;
                sceneManager.QuestSetNode(4);
                break;
            case 105:
                sceneManager.Task01 = true;
                sceneManager.QuestSetNode(5);
                break;
            case 106:
                sceneManager.Task01 = true;
                sceneManager.QuestSetNode(6);
                break;
        }
    }

}
