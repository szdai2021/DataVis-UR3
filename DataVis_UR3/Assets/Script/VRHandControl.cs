using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandControl : MonoBehaviour
{
    public GameObject palmCenter;

    public GameObject index;
    public GameObject ring;
    public GameObject pinky;
    public GameObject middle;

    public GameObject thumb;

    public float gestureThreshold = 0;

    public bool gestureDetection = false;

    public directionWheelControl DWC1;
    public directionWheelControl DWC2;

    public List<GameObject> DW_List;

    public float DW_Threshold = 0;

    public GameObject VRHandTwin;
    public GameObject handShadow;
    public Vector3 VRHandTwinPosOffset = new Vector3(0,0,0);
    public bool turnOnHandShadow = true;

    public int methodSwitch = 2;

    // Update is called once per frame
    void Update()
    {
        VRHandTwin.transform.position = this.gameObject.transform.position + VRHandTwinPosOffset;
        VRHandTwin.transform.rotation = this.gameObject.transform.rotation;

        if (Vector3.Distance(palmCenter.transform.position, index.transform.position) < gestureThreshold &
            Vector3.Distance(palmCenter.transform.position, ring.transform.position) < gestureThreshold &
            Vector3.Distance(palmCenter.transform.position, pinky.transform.position) < gestureThreshold &
            Vector3.Distance(palmCenter.transform.position, middle.transform.position) < gestureThreshold)
        {
            gestureDetection = true;
            DWC2.hide = false;
        }
        else
        {
            gestureDetection = false;
            DWC2.hide = true;
        }

        switch (methodSwitch)
        {
            case 1:
                thumbDirecting();
                break;
            case 2:
                centerDirecting();
                break;
            default:
                centerDirecting();
                break;
        }

        if (turnOnHandShadow)
        {
            if (gestureDetection)
            {
                handShadow.SetActive(false);
            }
            else
            {
                handShadow.SetActive(true);
            }
        }
    }

    private void centerDirecting()
    {
        if (gestureDetection)
        {
            DWC1.posSyc = false;

            //find the closest direction
            float minDistance = 100;
            GameObject closestDirection = null;
            foreach (GameObject g in DW_List)
            {
                if (Vector3.Distance(palmCenter.transform.position, g.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(palmCenter.transform.position, g.transform.position);
                    closestDirection = g;
                }
            }

            if (minDistance < DW_Threshold & closestDirection != null)
            {
                //change highlight color
                closestDirection.GetComponent<DW_IndivisualControl>().highlightFlag = true;

                //move vr hand
                VRHandTwinPosOffset += closestDirection.GetComponent<DW_IndivisualControl>().offset;
            }
            else
            {
                foreach (GameObject g in DW_List)
                {
                    //change highlight color
                    g.GetComponent<DW_IndivisualControl>().highlightFlag = false;
                }
            }
        }
        else
        {
            DWC1.posSyc = true;
        }
    }

    private void thumbDirecting()
    {

        if (gestureDetection)
        {
            //find the closest direction
            float minDistance = 100;
            GameObject closestDirection = null;
            foreach (GameObject g in DW_List)
            {
                if (Vector3.Distance(thumb.transform.position, g.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(thumb.transform.position, g.transform.position);
                    closestDirection = g;
                }
            }

            if (minDistance < DW_Threshold & closestDirection != null)
            {
                //change highlight color
                closestDirection.GetComponent<DW_IndivisualControl>().highlightFlag = true;

                //move vr hand
                VRHandTwinPosOffset += closestDirection.GetComponent<DW_IndivisualControl>().offset;
            }
            else
            {
                foreach (GameObject g in DW_List)
                {
                    //change highlight color
                    g.GetComponent<DW_IndivisualControl>().highlightFlag = false;
                }
            }
        }
    }
}
