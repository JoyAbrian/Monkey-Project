using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour{
    public Camera[] cctvCameras; // Array atau List kamera yang akan dipakai CCTV
    public SoundType[] soundLists; // Array atau List suara yang akan dipakai CCTV

    public RenderTexture cctvRenderTexture; // Render Texture yang akan digunakan oleh CCTV

    private int currentCameraIndex = 0; // Menyimpan indeks kamera yang sedang aktif

    private void Start()
    {
        SoundManager.PlayAmbience(soundLists[currentCameraIndex]);

        // Inisialisasi pertama, pastikan semua kamera nonaktif kecuali yang pertama
        for (int i = 0; i < cctvCameras.Length; i++)
        {
            if (i == currentCameraIndex)
            {
                ActivateCamera(cctvCameras[i]);
            }
            else
            {
                DeactivateCamera(cctvCameras[i]);
            }
        }
    }

    // Fungsi untuk berpindah ke kamera berikutnya
    public void SwitchToNextCamera()
    {
        SoundManager.StopAmbience();

        // Matikan kamera saat ini
        DeactivateCamera(cctvCameras[currentCameraIndex]);

        // Berpindah ke kamera berikutnya
        currentCameraIndex = (currentCameraIndex + 1) % cctvCameras.Length;

        // Aktifkan kamera berikutnya
        ActivateCamera(cctvCameras[currentCameraIndex]);
        SoundManager.PlayAmbience(soundLists[currentCameraIndex]);
    }

        public void SwitchToPreviousCamera()
    {
        // Matikan kamera saat ini
        DeactivateCamera(cctvCameras[currentCameraIndex]);

        // Berpindah ke kamera sebelumnya
        currentCameraIndex = (currentCameraIndex - 1 + cctvCameras.Length) % cctvCameras.Length;

        // Aktifkan kamera sebelumnya
        ActivateCamera(cctvCameras[currentCameraIndex]);
    }


    // Fungsi untuk mengaktifkan kamera dan mengatur target Render Texture
    private void ActivateCamera(Camera cam)
    {
        cam.targetTexture = cctvRenderTexture;
        cam.gameObject.SetActive(true);
    }

    // Fungsi untuk menonaktifkan kamera dan menghapus target Render Texture
    private void DeactivateCamera(Camera cam)
    {
        cam.targetTexture = null;
        cam.gameObject.SetActive(false);
    }
}