using UnityEngine;

public class Anomaly : MonoBehaviour
{
    // Enum untuk jenis anomali
    public enum AnomalyType
    {
        MovingObject,
        MissingObject,
        Light,
        Sound,
        Ghost
    }

    // Enum untuk nama ruangan
    public enum RoomName
    {
        Canteen,
        Hall,
        Hallway,
        PatientRoom,
        VipRoom
    }

    // Anomaly Type dan Room Name sekarang menggunakan enum
    public AnomalyType anomalyType;   // Pilih dari enum jenis anomali
    public RoomName roomName;         // Pilih dari enum nama ruangan
    
    private bool isTriggered = false; // Status apakah anomali sedang aktif
    private bool isReported = false;  // Status apakah anomali sudah dilaporkan
    private AnomalyItemController itemController;

    private void Start()
    {
        itemController = GetComponent<AnomalyItemController>();  // Pastikan AnomalyItemController ada di objek yang sama
    }

    public void TriggerAnomaly()
    {
        isTriggered = true;
        isReported = false;
        if (itemController != null)
        {
            itemController.TriggerAnomaly(); 
        }
        Debug.Log($"{anomalyType} has been trigger in {roomName}");
    }

    public void ResetAnomaly()
    {
        isTriggered = false;
        isReported = true;
        if (itemController != null)
        {
            itemController.ResetToInitialState(); 
        }
        Debug.Log($"{anomalyType} has been fixed in {roomName}");
    }

    public bool IsTriggered()
    {
        return isTriggered;
    }

    public bool IsReported()
    {
        return isReported;
    }
}
