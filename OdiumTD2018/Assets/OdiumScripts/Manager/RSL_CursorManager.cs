using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devdog.General.UI;
using Invector.vCharacterController;

public class RSL_CursorManager : MonoBehaviour {

    //static int windowCounter;
    public int windowCounter;
    static float lastWindowShowTime = 0.0f;

    public UIWindow[] uiWindows;
    public bool registered = true;
    public bool registerConfirm = true;
    public bool showDialogBox = false;
    public Texture2D cursorPointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject playerGO;
    public vThirdPersonInput inputController;
    public float registerTime;

    WaitForSeconds registerWait;

    void Start() {
        registerWait = new WaitForSeconds(registerTime);
        playerGO = GameObject.FindGameObjectWithTag("Player") as GameObject;
        if (playerGO) {
            inputController = playerGO.GetComponent<vThirdPersonInput>();
        }

        foreach(UIWindow w in uiWindows) {
            w.OnShow += () =>
            {
                windowCounter++;
                if (windowCounter > 0) {
                    registered = true;
                    StartCoroutine(AdjustCursorState());
                }
            };

            w.OnHide += () =>
            {
                windowCounter--;
                if (windowCounter <= 0) {
                    registered = false;
                    StartCoroutine(AdjustCursorState());
                    windowCounter = 0;
                }
            };
        }

        StartCoroutine(AdjustCursorState());
    }

    private void LateUpdate() {
        if ((registerConfirm && showDialogBox) || (registerConfirm && !showDialogBox) || (!registerConfirm && showDialogBox)) {
            inputController.ShowCursor(true);
            //inputController.lockCameraInput = true;
            inputController.SetLockCameraInput(true);
            inputController.SetLockBasicInput(true);
            inputController.cameraZoomInput.useInput = false;
            //inputController.lockInput = true;
            Cursor.SetCursor(cursorPointer, hotSpot, cursorMode);
        } else {
            inputController.ShowCursor(false);
            //inputController.lockCameraInput = false;
            inputController.SetLockCameraInput(false);
            inputController.SetLockBasicInput(false);
            inputController.cameraZoomInput.useInput = true;
            //inputController.lockInput = false;
        }
    }

    protected IEnumerator AdjustCursorState() {
        yield return registerWait;

        if (registered) {
            registerConfirm = true;
            inputController.ShowCursor(true);
            //inputController.lockCameraInput = true;
            //inputController.lockInput = true;
            inputController.SetLockCameraInput(true);
            inputController.SetLockBasicInput(true);
            inputController.cameraZoomInput.useInput = false;
            Cursor.SetCursor(cursorPointer, hotSpot, cursorMode);
        }
        else {
            registerConfirm = false;
            inputController.ShowCursor(false);
            //inputController.lockCameraInput = false;
            inputController.SetLockCameraInput(false);
            inputController.SetLockBasicInput(false);
            inputController.cameraZoomInput.useInput = true;
            //inputController.lockInput = false;
        }
    }
}
