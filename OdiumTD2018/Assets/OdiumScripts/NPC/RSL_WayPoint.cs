using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSL_WayPoint : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "wayPoint.png", true);
    }

}
