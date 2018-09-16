using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSL_DoorManager : MonoBehaviour {

    public RSL_DoorControl doorControl1;
    public RSL_DoorControl doorControl2;

    public string triggerName = "";

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(triggerName)) {
            if (doorControl1 != null) doorControl1.OpenDoor();
            if (doorControl2 != null) doorControl2.OpenDoor();
        }
    }

}
