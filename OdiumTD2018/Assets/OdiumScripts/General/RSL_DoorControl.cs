using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSL_DoorControl : MonoBehaviour {

    // 1 = x, 2 = y, 3 = z
    public int doorAxisID;

    public float translateValue;
    public float easeTime;
    public OTween.EaseType ease;
    public float waitTime;

    private Vector3 startLocalPos;
    private Vector3 endLocalPos;
    private WaitForSeconds doorWait;

    private void Start() {
        startLocalPos = transform.localPosition;
        gameObject.isStatic = false;
        doorWait = new WaitForSeconds(waitTime);
    }

    public void OpenDoor() {
        OTween.ValueTo(gameObject, ease, 0.0f, -translateValue, easeTime, 0.0f, "StartOpen", "UpdateOpenDoor", "EndOpen");
        GetComponent<AudioSource>().Play();
    }

    private void UpdateOpenDoor(float f) {
        switch(doorAxisID) {
            case 1:
                Vector3 pos1 = transform.TransformDirection(new Vector3(1, 0, 0));
                transform.localPosition = startLocalPos + pos1 * f;
                break;
            case 2:
                Vector3 pos2 = transform.TransformDirection(new Vector3(0, 1, 0));
                transform.localPosition = startLocalPos + pos2 * f;
                break;
            case 3:
                Vector3 pos3 = transform.TransformDirection(new Vector3(0, 0, 1));
                transform.localPosition = startLocalPos + pos3 * f;
                break;
        }
    }

    private void UpdateCloseDoor(float f) {
        switch (doorAxisID) {
            case 1:
                Vector3 pos1 = transform.TransformDirection(new Vector3(-f, 0, 0));
                transform.localPosition = endLocalPos - pos1; 
                break;
            case 2:
                Vector3 pos2 = transform.TransformDirection(new Vector3(0, -f, 0));
                transform.localPosition = endLocalPos - pos2;
                break;
            case 3:
                Vector3 pos3 = transform.TransformDirection(new Vector3(0, 0, -f));
                transform.localPosition = endLocalPos - pos3;
                break;
        }
    }

    private void EndOpen() {
        endLocalPos = transform.localPosition;
        StartCoroutine(WaitToClose());
    }

    private IEnumerator WaitToClose() {

        yield return doorWait;
        OTween.ValueTo(gameObject, ease, 0.0f, translateValue, easeTime, 0.0f, "StartClose", "UpdateCloseDoor", "EndClose");
        GetComponent<AudioSource>().Play();
    }

}
