using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public List<Camera> cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Count;

        // Activate the new current camera
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}