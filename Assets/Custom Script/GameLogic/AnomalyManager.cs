using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    public List<Anomaly> anomalies; // Semua anomali yang ada di game

    private void Start()
    {
        foreach (var anomaly in anomalies)
        {
            anomaly.TriggerAnomaly();  // Memulai dengan semua anomali diaktifkan
        }
    }

    // Memvalidasi laporan pemain
    public bool ValidateReport(string roomName, string anomalyType)
    {
        foreach (var anomaly in anomalies)
        {
            if (anomaly.roomName == roomName && anomaly.anomalyType == anomalyType && anomaly.IsTriggered())
            {
                anomaly.ResetAnomaly();
                return true;
            }
        }
        return false;
    }

    // Mengecek apakah ada anomali yang belum dilaporkan
    public Anomaly CheckUnreportedAnomaly()
    {
        foreach (var anomaly in anomalies)
        {
            if (anomaly.IsTriggered() && !anomaly.IsReported())
            {
                return anomaly;
            }
        }
        return null;
    }
}
