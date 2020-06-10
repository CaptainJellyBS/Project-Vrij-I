using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Note: This script could create a lot of lag if there are shittons of pipes in the area, and I don't really know if it's a good idea to have the script in the first place.
//It also doesn't really work perfectly, because bounding boxes of colliders seem to be quite off, but *shrug*, it works well enough for now
public class AudioSourceCalculation : MonoBehaviour
{
    Collider col;
    MultipleFrogs frogs;
    GameObject curFrog;

    void Start()
    {
        frogs = FindObjectOfType<MultipleFrogs>();
        col = GetComponentInParent<Collider>();
    }

    void Update()
    {
        //Set the position of the audiosource to the point on the collider that is closest to the frog
        curFrog = frogs.activefrog;
        transform.position = col.ClosestPointOnBounds(curFrog.transform.position);
    }

}
