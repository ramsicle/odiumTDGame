using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RSL_NPCAI : MonoBehaviour {

    public enum Actions {
        idle,
        goToPlayer,
        goToPos,
        wander
    }
    public Actions npcAction;
    public bool alive;
    public float speedDampTime = 0.1f;              // Damping time for the Speed parameter.
    public float angularSpeedDampTime = 0.7f;       // Damping time for the AngularSpeed parameter
    public float angleResponseTime = 0.6f;// Response time for turning an angle into angularSpeed.
    public float deadZone = 5f;
    public NavMeshAgent nav;
    public Transform playerT;
    public float wanderSpeed = 2f;
    public float runSpeed = 5f;
    public float runWaitTime = 5f;
    public float wanderWaitTime = 1f;
    public static float wanderTimer;
    public int wayPointIndex;
    public int actions = 0;
    public bool actionComplete = false;
    public bool playerSighted = false;
    public Transform[] wayPoints;
    public Transform[] actionPos;
    public float[] actionWaitTimes;
    public RSL_NPCSensorFwd sensorFwdScript;

    // Create the parameters to pass to the helper function.
    private float speed;
    private float angle;
    private float rotSpeed = 2f;
    private Animator anim;                          // Reference to the animator component.
    private RSL_NPCHashID hash;                       // Reference to the HashIDs script.
    private float runTimer;
    private Vector3 goToPosition;
    private float goToPosWaitTime;

    private void Awake() {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        hash = GetComponent<RSL_NPCHashID>();
        anim = GetComponent<Animator>();

        nav.updateRotation = false;
        deadZone *= Mathf.Deg2Rad;
    }

    private void Start() {
        alive = true;
        npcAction = Actions.wander;
        StartCoroutine(NPC_FSM());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            actions++;
            if (actions > 7) {
                actions = 1;
            }
            SetAction(actions);
        }
    }

    public void SetGoToPos(Transform pos) {
        goToPosition = pos.position;
    }

    public void SetWaitTime(float wait) {
        goToPosWaitTime = wait;
    }

    private IEnumerator NPC_FSM() {
        while (alive) {
            switch (npcAction) {
                case Actions.idle: // go idle
                    transform.LookAt(playerT);
                    nav.isStopped = true;
                    nav.speed = 0;

                    speed = 0f;
                    angle = FindAngle(transform.forward, playerT.position - transform.position,
                    transform.up);
                    Setup(speed, angle);
                    break;
                case Actions.goToPlayer: // approach player
                    nav.isStopped = false;
                    nav.speed = wanderSpeed;
                    nav.SetDestination(playerT.position);
                    if (nav.remainingDistance <= nav.stoppingDistance) {
                        nav.isStopped = true;
                    }

                    speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
                    angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
                    if (Mathf.Abs(angle) < deadZone) {
                        transform.LookAt(transform.position + nav.desiredVelocity);
                        angle = 0f;
                    }
                    Setup(speed, angle);
                    break;
                case Actions.wander: // wander
                    nav.isStopped = false;
                    nav.speed = wanderSpeed;
                    if (nav.remainingDistance < nav.stoppingDistance) {
                        wanderTimer += Time.deltaTime;
                        if (wanderTimer >= wanderWaitTime) {
                            if (wayPointIndex == wayPoints.Length - 1)
                                wayPointIndex = 0;
                            else
                                wayPointIndex++;
                        }
                    }
                    nav.destination = wayPoints[wayPointIndex].position;

                    speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
                    angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
                    if (Mathf.Abs(angle) < deadZone) {
                        transform.LookAt(transform.position + nav.desiredVelocity);
                        angle = 0f;
                    }
                    Setup(speed, angle);
                    break;
                case Actions.goToPos:
                    nav.isStopped = false;
                    nav.speed = wanderSpeed;
                    //yield return new WaitForSeconds(goToPosWaitTime);
                    nav.SetDestination(goToPosition);
                    if (nav.remainingDistance <= nav.stoppingDistance) {
                        nav.isStopped = true;
                    }

                    speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
                    angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
                    if (Mathf.Abs(angle) < deadZone) {
                        transform.LookAt(transform.position + nav.desiredVelocity);
                        angle = 0f;
                    }
                    Setup(speed, angle);
                    break;
            }
            yield return null;
        }
    }

    public void SetAction(int act) {
        switch (act) {
            case 1:
                goToPosition = actionPos[0].position;
                goToPosWaitTime = actionWaitTimes[0];
                sensorFwdScript.goToPlayer = false;
                npcAction = RSL_NPCAI.Actions.goToPos;
                break;
            case 2:
                goToPosition = actionPos[1].position;
                goToPosWaitTime = actionWaitTimes[1];
                sensorFwdScript.goToPlayer = false;
                npcAction = RSL_NPCAI.Actions.goToPos;
                break;
            case 3:
                goToPosition = actionPos[2].position;
                goToPosWaitTime = actionWaitTimes[2];
                sensorFwdScript.goToPlayer = false;
                npcAction = RSL_NPCAI.Actions.goToPos;
                break;
            case 4:
                goToPosition = actionPos[3].position;
                goToPosWaitTime = actionWaitTimes[3];
                sensorFwdScript.goToPlayer = false;
                npcAction = RSL_NPCAI.Actions.goToPos;
                break;
            case 5:
                goToPosition = actionPos[4].position;
                goToPosWaitTime = actionWaitTimes[4];
                sensorFwdScript.goToPlayer = false;
                npcAction = RSL_NPCAI.Actions.goToPos;
                break;
            case 6:
                sensorFwdScript.goToPlayer = false;
                npcAction = RSL_NPCAI.Actions.wander;
                break;
            case 7:
                sensorFwdScript.goToPlayer = true;
                npcAction = RSL_NPCAI.Actions.goToPlayer;
                break;
        }
    }

    private void OnAnimatorMove() {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    public void Setup(float speedSetup, float angleSetup) {
        // Angular speed is the number of degrees per second.
        float angularSpeed = angleSetup / angleResponseTime;

        // Set the mecanim parameters and apply the appropriate damping to them.
        anim.SetFloat(hash.speedFloat, speedSetup, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector) {
        // If the vector the angle is being calculated to is 0...
        if (toVector == Vector3.zero)
            // ... the angle between them is 0.
            return 0f;

        // Create a float to store the angle between the facing of the enemy and the 
        // direction it's travelling.
        float angle = Vector3.Angle(fromVector, toVector);

        // Find the cross product of the two vectors (this will point up if the velocity 
        // is to the right of forward).
        Vector3 normal = Vector3.Cross(fromVector, toVector);

        // The dot product of the normal with the upVector will be positive if they point 
        // in the same direction.
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));

        // We need to convert the angle we've found from degrees to radians.
        angle *= Mathf.Deg2Rad;

        return angle;
    }

}
