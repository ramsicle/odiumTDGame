using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RSL_ShowText : MonoBehaviour {

    public Text iconNameTxt;

    private void OnMouseOver() {
        iconNameTxt.enabled = true;
    }

    private void OnMouseExit() {
        iconNameTxt.enabled = false;
    }

    //private void Update() {
    //    if (Input.GetMouseButtonDown(0)) {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit, 100)) {
    //            Debug.Log("GameObject Hit: " + hit.collider.gameObject.name.ToString());
    //        }
    //    }
    //}

    public void TestButton() {
        Debug.Log("Button has been pressed...");
    }

}
