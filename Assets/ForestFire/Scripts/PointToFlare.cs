using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToFlare : MonoBehaviour
{
    // target is the flare, we get the transform of the flare
    private Transform Target;
    // the main script file of the game
    public ForestFire3D forestFire3D;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // get the spawned flare which initiates in the main script
        Target = forestFire3D.spawnFlare.transform;

        // get the direction from the arrow to the flare
        Vector3 direction = Target.position - this.gameObject.transform.position;
        // identify the rotation
        Quaternion rotation = Quaternion.LookRotation(direction);
        // transform rotate the arrow to the direction of the spawned flare
        transform.rotation = rotation;
    }
}
