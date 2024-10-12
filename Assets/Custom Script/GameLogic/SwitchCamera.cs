using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Material[] cameraList;
    public int currentCameraIndex = 0;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        objectRenderer.material = cameraList[currentCameraIndex];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PreviousCamera();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            NextCamera();
        }

        objectRenderer.material = cameraList[currentCameraIndex];
    }

    public void NextCamera()
    {
        if (currentCameraIndex + 1 < cameraList.Length)
        {
            currentCameraIndex++;
        }
        else
        {
            currentCameraIndex = 0;
        }
    }

    public void PreviousCamera()
    {
        if (currentCameraIndex - 1 >= 0)
        {
            currentCameraIndex--;
        }
        else
        {
            currentCameraIndex = cameraList.Length - 1;
        }
    }
}