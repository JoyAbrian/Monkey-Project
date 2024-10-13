using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Material[] cameraList;
    public SoundType[] cameraSounds;
    public int currentCameraIndex = 0;

    private Renderer objectRenderer;

    private void Start()
    {
        SoundManager.PlayAmbience(SoundType.AmbienceControlRoom, 1f);

        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = cameraList[currentCameraIndex];

        PlayCameraSound();
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
        PlayCameraSound();
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
        PlayCameraSound();
    }

    private void PlayCameraSound()
    {
        SoundManager.StopSound();

        if (cameraSounds != null && cameraSounds.Length > currentCameraIndex)
        {
            SoundManager.PlaySound(cameraSounds[currentCameraIndex], 0.3f);
        }
    }
}