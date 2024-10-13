using UnityEngine;

public class AnomalyReport : MonoBehaviour
{
    public AnomalyManager anomalyManager;

    public void ReportAnomalies(Anomaly.RoomName roomName, Anomaly.AnomalyType anomalyType)
    {
        if (anomalyManager.ValidateReport(roomName, anomalyType))
        {
            Debug.Log($"Anomali {anomalyType} di ruangan {roomName} telah dilaporkan dan diperbaiki.");
        }
        else
        {
            Debug.Log($"Tidak ada anomali yang cocok di ruangan {roomName} dengan jenis {anomalyType}.");
        }
    }
}
