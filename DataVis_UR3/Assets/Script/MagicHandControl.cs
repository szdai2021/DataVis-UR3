using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHandControl : MonoBehaviour
{
    public GameObject sphere;
    public GameObject sphereTwin;

    public GameObject VRHand;
    public GameObject VRHandTwin;

    public UnityClient unityClient;

    public BoxCollider robotRange;

    public GameObject sliderReference;
    public GameObject robotEndEffector;

    private bool prev_gestureDetection = false;
    private bool current_gestureDetection = false;

    private Vector3 prev_sphereTwin = new Vector3(0,0,0);

    // Update is called once per frame
    void Update()
    {
        current_gestureDetection = VRHand.GetComponent<VRHandControl>().gestureDetection;

        if (current_gestureDetection == false & prev_gestureDetection == true)
        {
            sphereTwin.transform.position = sphere.transform.position - VRHandTwin.transform.position + VRHand.transform.position;
        }

        if (robotRange.bounds.Contains(sphereTwin.transform.position) & Vector3.Distance(prev_sphereTwin, sphereTwin.transform.position)>0.0001)
        {
            sliderReference.transform.position = sphereTwin.transform.position;

            unityClient.initialPos();
            Vector3 newPos = unityClient.convertUnityCoord2RobotCoord(robotEndEffector.transform.position);

            unityClient.customMove(newPos.x, newPos.y, newPos.z, -0.6, 1.47, 0.62, movementType: 1);

            prev_sphereTwin = sphereTwin.transform.position;
        }

        prev_gestureDetection = current_gestureDetection;
    }

    public static (Vector3, Quaternion) getNewPosRot(Transform T_from, Transform T_to, Transform target)
    {
        Vector3 posInDestination = Vector3.zero;
        Quaternion angleInDestination = Quaternion.identity;

        Vector3 offset = T_from.position - T_to.position;
        posInDestination = target.position + offset;

        float angularDifference = Quaternion.Angle(T_from.rotation, T_to.rotation);

        Quaternion rotationalDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newDirection = rotationalDifference * target.forward;
        angleInDestination = Quaternion.LookRotation(newDirection, Vector3.up);

        return (posInDestination,angleInDestination);
    }

}
