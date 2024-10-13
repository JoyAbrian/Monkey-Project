using UnityEngine;
using UnityEngine.UI;

public class ReportAnomaly : MonoBehaviour 
{
    // Referensi untuk dropdown di Inspector
    public TMPro.TMP_Dropdown anomalyTypeDropdown;
    public TMPro.TMP_Dropdown roomNameDropdown;

    // Referensi untuk AnomalyReport
    public AnomalyReport anomalyReport;

    // Fungsi ini akan dipanggil saat tombol Report diklik
    public void OnReportButtonClick()
    {
        // Mendapatkan nilai dropdown berdasarkan pilihan indeks
        string selectedAnomalyType = anomalyTypeDropdown.options[anomalyTypeDropdown.value].text;
        string selectedRoomName = roomNameDropdown.options[roomNameDropdown.value].text;

        Debug.Log("Selected Anomaly Type: " + selectedAnomalyType);
        Debug.Log("Selected Room Name: " + selectedRoomName);

        // Konversi nilai dropdown menjadi enum
        Anomaly.AnomalyType anomalyType = ConvertToAnomalyType(selectedAnomalyType);
        Anomaly.RoomName roomName = ConvertToRoomName(selectedRoomName);

        // Lakukan sesuatu dengan nilai yang dipilih, misalnya kirim laporan ke AnomalyManager
        Debug.Log("Reported Anomaly: " + anomalyType + " in " + roomName);

        // Kirim laporan ke AnomalyReport
        anomalyReport.ReportAnomalies(roomName, anomalyType);
    }

    // Fungsi untuk mengonversi teks dropdown menjadi enum AnomalyType
    private Anomaly.AnomalyType ConvertToAnomalyType(string anomalyTypeText)
    {
        switch (anomalyTypeText)
        {
            case "MovingObject":
                return Anomaly.AnomalyType.MovingObject;
            case "MissingObject":
                return Anomaly.AnomalyType.MissingObject;
            case "Light":
                return Anomaly.AnomalyType.Light;
            case "Ghost":
                return Anomaly.AnomalyType.Ghost;
            default:
                Debug.LogWarning("Tipe anomali tidak dikenali: " + anomalyTypeText);
                return Anomaly.AnomalyType.MovingObject;  // Default fallback
        }
    }

    // Fungsi untuk mengonversi teks dropdown menjadi enum RoomName
    private Anomaly.RoomName ConvertToRoomName(string roomNameText)
    {
        switch (roomNameText)
        {
            case "Hall":
                return Anomaly.RoomName.Hall;
            case "VIP Room":
                return Anomaly.RoomName.VipRoom;
            case "Patient Room":
                return Anomaly.RoomName.PatientRoom;
            case "Canteen":
                return Anomaly.RoomName.Canteen;
            case "Hallway":
                return Anomaly.RoomName.Hallway;
            default:
                Debug.LogWarning("Nama ruangan tidak dikenali: " + roomNameText);
                return Anomaly.RoomName.Hall;  // Default fallback
        }
    }
}
