using System.Collections.Generic;
using UnityEngine;

public class AnomalyItemController : MonoBehaviour
{
    [Header("Anomaly Settings")]
    public Anomaly.AnomalyType anomalyType;  // Tipe anomali yang diatur berdasarkan enum

    // Setting untuk anomali tipe Light
    [Header("Light Settings")]
    public Light targetLight;  // Lampu yang akan dinonaktifkan saat anomali

    // Setting untuk anomali tipe Ghost
    [Header("Ghost Settings")]
    public GameObject ghostPrefab;  // Prefab hantu yang akan dimunculkan
    public List<Transform> ghostPositions;  // Daftar posisi dan rotasi yang terdaftar untuk ghost
    private GameObject spawnedGhost;  // Referensi ke ghost yang sudah di-spawn

    // Setting untuk anomali MovingObject
    [Header("Moving Object Settings")]
    public Vector3 anomalyPosition;  // Posisi baru item saat anomali
    public Vector3 anomalyRotation;  // Rotasi baru item saat anomali
    private Vector3 initialPosition;  // Posisi awal item
    private Quaternion initialRotation;  // Rotasi awal item

    // Setting untuk anomali MissingObject
    [Header("Missing Object Settings")]
    public bool isVisibleDuringAnomaly = true;  // Apakah item terlihat selama anomali

    private void Start()
    {
        // Inisialisasi kondisi awal berdasarkan tipe anomali
        switch (anomalyType)
        {
            case Anomaly.AnomalyType.Light:
                if (targetLight != null)
                {
                    Debug.Log("Initializing Light anomaly: Light is currently ON.");
                    targetLight.enabled = true;  // Lampu dinyalakan pada awalnya
                }
                break;

            case Anomaly.AnomalyType.Ghost:
                if (ghostPrefab != null)
                {
                    Debug.Log("Initializing Ghost anomaly: Ghost is hidden.");
                    ghostPrefab.SetActive(false);  // Ghost tidak muncul pada awalnya
                }
                break;

            case Anomaly.AnomalyType.MovingObject:
                // Simpan posisi dan rotasi awal
                initialPosition = transform.position;
                initialRotation = transform.rotation;
                Debug.Log($"Initializing MovingObject anomaly: Initial Position set at {initialPosition}, Rotation set at {initialRotation.eulerAngles}");
                break;

            case Anomaly.AnomalyType.MissingObject:
                // Atur visibilitas berdasarkan isVisibleDuringAnomaly
                gameObject.SetActive(!isVisibleDuringAnomaly);
                Debug.Log($"Initializing MissingObject anomaly: Object visibility is set to {!isVisibleDuringAnomaly}");
                break;

            case Anomaly.AnomalyType.Sound:
                // Logika SoundManager bisa ditambahkan nanti
                Debug.Log("Initializing Sound anomaly.");
                break;
        }
    }

    // Fungsi untuk memicu anomali
    public void TriggerAnomaly()
    {
        switch (anomalyType)
        {
            case Anomaly.AnomalyType.Light:
                if (targetLight != null)
                {
                    Debug.Log("Light anomaly triggered: Turning off light");
                    targetLight.enabled = false;  // Nonaktifkan lampu
                }
                break;

            case Anomaly.AnomalyType.Ghost:
                if (ghostPrefab != null && ghostPositions.Count > 0)
                {
                    int randomIndex = Random.Range(0, ghostPositions.Count);  // Pilih posisi ghost secara acak
                    Transform ghostPosition = ghostPositions[randomIndex];  // Ambil posisi acak dari list
                    if (spawnedGhost == null)
                    {
                        // Spawn ghost di posisi yang dipilih
                        spawnedGhost = Instantiate(ghostPrefab, ghostPosition.position, ghostPosition.rotation);
                    }
                    else
                    {
                        // Jika ghost sudah muncul, pindahkan ke posisi baru
                        spawnedGhost.transform.position = ghostPosition.position;
                        spawnedGhost.transform.rotation = ghostPosition.rotation;
                        spawnedGhost.SetActive(true);  // Aktifkan ghost
                    }
                    Debug.Log("Ghost anomaly triggered: Ghost appeared at a new position.");
                }
                break;

            case Anomaly.AnomalyType.MovingObject:
                Debug.Log($"MovingObject anomaly triggered: Moving object to {anomalyPosition}.");
                // Pindahkan objek ke posisi dan rotasi anomaly
                transform.position = anomalyPosition;
                transform.rotation = Quaternion.Euler(anomalyRotation);
                break;

            case Anomaly.AnomalyType.MissingObject:
                Debug.Log("MissingObject anomaly triggered: Hiding object.");
                // Sembunyikan objek selama anomali
                gameObject.SetActive(false);
                break;

            case Anomaly.AnomalyType.Sound:
                Debug.Log("Sound anomaly triggered.");
                // Logika untuk Sound bisa ditambahkan ketika SoundManager tersedia
                break;
        }
    }

    // Fungsi untuk mereset item ke posisi awal
    public void ResetToInitialState()
    {
        switch (anomalyType)
        {
            case Anomaly.AnomalyType.Light:
                if (targetLight != null)
                {
                    Debug.Log("Reset Light: Turning on light");
                    targetLight.enabled = true;  // Nyalakan kembali lampu
                }
                break;

            case Anomaly.AnomalyType.Ghost:
                if (spawnedGhost != null)
                {
                    Debug.Log("Reset Ghost: Disabling ghost");
                    spawnedGhost.SetActive(false);  // Sembunyikan ghost
                }
                break;

            case Anomaly.AnomalyType.MovingObject:
                Debug.Log($"Reset MovingObject: Resetting to initial position {initialPosition} and rotation {initialRotation.eulerAngles}");
                // Kembalikan objek ke posisi dan rotasi awal
                transform.position = initialPosition;
                transform.rotation = initialRotation;
                break;

            case Anomaly.AnomalyType.MissingObject:
                Debug.Log("Reset MissingObject: Making object visible.");
                // Tampilkan kembali objek
                gameObject.SetActive(true);
                break;

            case Anomaly.AnomalyType.Sound:
                Debug.Log("Reset Sound.");
                // Logika reset untuk Sound bisa ditambahkan ketika SoundManager tersedia
                break;
        }
    }
}
