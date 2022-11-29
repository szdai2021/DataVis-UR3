using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public GameObject to;
    public GameObject from;
    public GameObject target;
    public GameObject newTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        (newTarget.transform.position, newTarget.transform.rotation) = MagicHandControl.getNewPosRot(from.transform, to.transform, target.transform);
    }
}
