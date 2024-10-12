using UnityEngine;

public class AnomalyItemController : MonoBehaviour
{
    [Header("Anomaly Settings")]
    [Tooltip("Posisi baru item saat anomali terjadi")]
    public Vector3 anomalyPosition;  // Posisi baru item saat anomali

    [Tooltip("Rotasi baru item saat anomali terjadi")]
    public Vector3 anomalyRotation;  // Rotasi baru item saat anomali

    [Tooltip("Apakah item terlihat selama anomali")]
    public bool isVisibleDuringAnomaly = true;  // Apakah item terlihat selama anomali

    // Variabel untuk menyimpan state awal
    private Vector3 initialPosition;  // Posisi awal item
    private Quaternion initialRotation;  // Rotasi awal item

    private void Start()
    {
        // Menyimpan posisi dan rotasi awal dari item
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // Fungsi untuk memicu anomali (mengubah posisi dan rotasi item)
    public void TriggerAnomaly()
    {
        // Mengubah posisi dan rotasi item ke posisi dan rotasi anomaly
        transform.position = anomalyPosition;
        transform.rotation = Quaternion.Euler(anomalyRotation);
        
        // Mengatur visibilitas item saat anomali
        gameObject.SetActive(isVisibleDuringAnomaly);
    }

    // Fungsi untuk mengembalikan item ke posisi dan rotasi awal
    public void ResetToInitialPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        
        // Mengembalikan item agar terlihat kembali
        gameObject.SetActive(true);
    }
}

