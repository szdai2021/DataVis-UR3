using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera portalCamera;

    public Material cameraMaterial;

    public bool resetCamera = false;

    // Start is called before the first frame update
    void Update()
    {
        if (resetCamera)
        {

            if (portalCamera.targetTexture != null)
            {
                portalCamera.targetTexture.Release();
            }
            portalCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            cameraMaterial.mainTexture = portalCamera.targetTexture;

            resetCamera = false;
        }
    }

}
