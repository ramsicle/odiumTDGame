using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class RSL_SceneUIManager : MonoBehaviour {

    public GameObject containerDialogue;
    public VIDE_Assign npcVideAssign;
    //public GameObject containerPlayer;
    public Text txtNPC;
    public Text[] txtPlayerChoices;
    public RSL_CursorManager cursorManager;
    public Button nextBtn;
    public Text npcNameTxt;
    public Text playerNameTxt;

    public int checkPoint;

    public bool Task01 = false;
    public bool Task02 = false;
    public bool Task03 = false;
    public bool Task04 = false;
    public bool Task05 = false;
    public bool Task06 = false;

    private void Awake() {
        cursorManager = FindObjectOfType<RSL_CursorManager>();
    }

    private void Start() {
        containerDialogue.SetActive(false);
        playerNameTxt.text = "Marcus";
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (!VD.isActive) {
                Begin();
            } else {
                VD.Next();
            }
        }
    }

    private void Begin() {
        VD.OnActionNode += ActionUI;
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        ResetPlayerChoiceTxt();
        containerDialogue.SetActive(true);
        cursorManager.showDialogBox = true;
        if (npcVideAssign != null)
            VD.BeginDialogue(npcVideAssign);

        VD.SetNode(checkPoint);
    }

    private void ActionUI(int nodeID) {
        Debug.Log("Action node: " + nodeID);
    }

    private void UpdateUI(VD.NodeData data) {
        
        if (data.isPlayer) {
            ResetPlayerChoiceTxt();
            nextBtn.enabled = false;
            for(int i=0; i<txtPlayerChoices.Length; i++) {
                if (i < data.comments.Length) {
                    txtPlayerChoices[i].transform.parent.gameObject.SetActive(true);
                    txtPlayerChoices[i].text = data.comments[i];
                } else {
                    txtPlayerChoices[i].transform.parent.gameObject.SetActive(false);
                }
            }
        } else {
            ResetPlayerChoiceTxt();

            nextBtn.enabled = true;
            txtNPC.text = data.comments[data.commentIndex];
            npcNameTxt.text = VD.assigned.alias;
        }
    }

    private void End(VD.NodeData data) {
        cursorManager.showDialogBox = false;
        containerDialogue.SetActive(false);
        VD.OnActionNode -= ActionUI;
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    private void OnDisable() {
        VD.OnActionNode -= ActionUI;
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
        if (containerDialogue != null) End(null);
    }

    private void ResetPlayerChoiceTxt() {
        for (int i=0; i<txtPlayerChoices.Length; i++) {
            txtPlayerChoices[i].text = "";
        }
    }

    public void SetPlayerChoice(int choice) {
        VD.nodeData.commentIndex = choice;
        VD.Next();
    }

    public void NextVD() {
        VD.Next();
    }

    public void SetNodeStart(int val) {
        checkPoint = val;
    }

    public void QuestSetNode(int val) {
        switch(val) {
            case 1:
                if (Task01) SetNodeStart(27);
                else SetNodeStart(28);
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
        }
    }
}
