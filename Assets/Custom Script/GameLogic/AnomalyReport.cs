using UnityEngine;

public class AnomalyReport : MonoBehaviour
{
    public AnomalyManager anomalyManager;

    // Fungsi untuk melaporkan bahwa semua anomali telah diperbaiki
    public void ReportAnomalies()
    {
        Debug.Log("Anomali dilaporkan. Mengembalikan semua objek ke state awal.");
        anomalyManager.ResetAnomalies();
    }
}
