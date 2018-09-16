using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSL_NPCHashID : MonoBehaviour {
    
    public int locomotionState;
    public int speedFloat;
    public int angularSpeedFloat;
    public int action;
    public int actionState;
    public int crouchState;

    private void Awake() {
        locomotionState = Animator.StringToHash("Base Layer.Locomotion.Free Locomotion.Free Locomotion");
        speedFloat = Animator.StringToHash("Speed");
        angularSpeedFloat = Animator.StringToHash("AngularSpeed");
        action = Animator.StringToHash("Action");
        actionState = Animator.StringToHash("Actions");
        crouchState = Animator.StringToHash("Base Layer.Locomotion.Free Locomotion.Free Crouch");
    }

}
