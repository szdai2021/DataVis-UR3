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
            (sphereTwin.transform.position, sphereTwin.transform.rotation) = getTargetPosRot(VRHandTwin.transform, VRHand.transform, sphere.transform);
            //sphereTwin.transform.position = sphere.transform.position - VRHandTwin.transform.position + VRHand.transform.position;
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

    public static (Vector3 targetPos,Quaternion targetRot) getTargetPosRot(Transform T_from, Transform T_to, Transform source)
    {
        Vector3 targetPos;
        Quaternion targetRot;

        Matrix4x4 transformation = Matrix4x4.identity;

        transformation = T_from.worldToLocalMatrix * T_to.localToWorldMatrix;

        targetPos = T_to.TransformPoint(T_from.InverseTransformPoint(source.position));

        targetRot = source.rotation * transformation.rotation;

        return (targetPos, targetRot);
    }

}
