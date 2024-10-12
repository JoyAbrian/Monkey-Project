using UnityEngine;

public class Anomaly : MonoBehaviour
{
    public string anomalyType;  // Jenis anomali (misal: "Item", "Light", dll.)
    public string roomName;     // Nama ruangan (misal: "Cafetaria", "Room", dll.)
    
    private bool isTriggered = false; // Status apakah anomali sedang aktif
    private bool isReported = false;  // Status apakah anomali sudah dilaporkan

    public void TriggerAnomaly()
    {
        isTriggered = true;
        isReported = false;
    }

    public void ResetAnomaly()
    {
        isTriggered = false;
        isReported = true;
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
