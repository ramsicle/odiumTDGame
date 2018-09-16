using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class RSL_NPCSensorFwd : MonoBehaviour {

    public GameObject player;
    public Transform playerT;
    public float FOVAngle = 110f; // field of view angle
    public Vector3 playerCurrentPosition;
    public Vector3 playerLastPosition;
    public Animator anim;
    public Animator playerAnim;
    public NavMeshAgent nav;
    public RSL_NPCAI npcAIScript;
    public SphereCollider col;
    public RSL_NPCHashID hashID;
    public Transform npcT;
    public float waitTime;
    public bool goToPlayer = false;
    public bool stealthMode = false;
    public bool playerInLOS = false;
    public bool stealthModeEffective = false;
    public GameObject tmp;
    public TextMeshPro tmpTxt;
    public RSL_SceneUIManager sceneUIManager;
    public VIDE_Assign oldGuyVideAssign;
    //public GameObject dialogueBox;
    //public bool showDialogueBox;

    //public RSL_CursorManager cursorManager;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.transform;
        playerAnim = player.GetComponent<Animator>();
    }

    //private void Start() {
    //    cursorManager = RSL_CursorManager.FindObjectOfType<RSL_CursorManager>();
    //}

    private void OnTriggerStay(Collider other) {
        if (goToPlayer) {
            if (other.gameObject == player) {
                //Debug.Log("playr triggered");
                tmp.SetActive(true);
                npcAIScript.playerSighted = false;
                float pl = PathLength(player.transform.position);
                //Debug.Log("Path length: " + pl);
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                if (angle < FOVAngle * 0.5f) {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, direction, out hit, 20)) {
                        //Debug.Log("Hit: " + hit.collider.name.ToString());
                        if (hit.collider.gameObject == player) {
                            playerInLOS = true;
                        } else {
                            playerInLOS = false;
                        }
                    }
                }
                if (pl <= 10) {
                    stealthModeEffective = true;
                } else {
                    stealthModeEffective = false;
                }

                if (playerInLOS) {
                    if (pl < 2) {
                        npcAIScript.playerSighted = true;
                        npcAIScript.npcAction = RSL_NPCAI.Actions.idle;
                        playerCurrentPosition = other.gameObject.transform.position;
                    }
                } else {
                    if (stealthModeEffective) {
                        if (stealthMode) {
                            npcAIScript.playerSighted = false;
                            npcAIScript.npcAction = RSL_NPCAI.Actions.wander;
                            playerLastPosition = playerCurrentPosition;
                        } else {
                            if (pl < 2) {
                                npcAIScript.playerSighted = true;
                                npcAIScript.npcAction = RSL_NPCAI.Actions.idle;
                                playerCurrentPosition = other.gameObject.transform.position;
                            }
                        }
                    } else {
                        npcAIScript.playerSighted = false;
                        npcAIScript.npcAction = RSL_NPCAI.Actions.wander;
                        playerLastPosition = playerCurrentPosition;
                    }
                }

                if (pl < 2) {
                    tmpTxt.text = "Press T";
                    sceneUIManager.npcVideAssign = oldGuyVideAssign;
                } else {
                    tmpTxt.text = "Talk";
                }

                //if (Input.GetKeyDown(KeyCode.T)) {
                    //showDialogueBox = !showDialogueBox;
                    //if (showDialogueBox) {
                        //dialogueBox.SetActive(true);
                        //cursorManager.registerConfirm = true;
                    //}
                    //else {
                    //    dialogueBox.SetActive(false);
                    //    cursorManager.registerConfirm = false;
                    //}
                //}
            }
            int playerLayer0StateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            //int playerLayer1StateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;
            //Debug.Log(":" + playerLayer0StateHash);

            if (playerLayer0StateHash == hashID.crouchState) {
                stealthMode = true;
            }
            else {
                //Debug.Log("Player is in stealth mode");
                stealthMode = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            Debug.Log("Player exit trigger zone...");
            npcAIScript.playerSighted = false;
            npcAIScript.npcAction = RSL_NPCAI.Actions.wander; 

            tmp.SetActive(false);
        }
    }

    /// <summary>
    /// Calculate the path to player using nav path
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    private float PathLength(Vector3 targetPos) {
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled) nav.CalculatePath(targetPos, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = npcT.position;
        allWayPoints[allWayPoints.Length - 1] = targetPos;

        for (int i = 0; i < path.corners.Length; i++) {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0;

        for (int i = 0; i < allWayPoints.Length - 1; i++) {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }

}
