using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Device Active: " + XRSettings.isDeviceActive);
        Debug.Log("Device Name: " + XRSettings.loadedDeviceName);

        if(!XRSettings.isDeviceActive)
        {
            Debug.Log("No VR Device Detected");
        }
        else if(XRSettings.isDeviceActive && XRSettings.loadedDeviceName == "MockHMD")
        {
            Debug.Log("Mock HMD Detected");
        }
        else
        {
            Debug.Log("VR Device Detected");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
