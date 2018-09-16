using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSL_QuestSceneManager : MonoBehaviour {

    public bool[] tasks;

    public void SetTaskComplete(int task) {
        for(int i=0; i<tasks.Length; i++) {
            if (i == task) tasks[i] = true;
        }
    }

}
