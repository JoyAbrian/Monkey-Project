using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : MonoBehaviour
{
    // Dictionary untuk menyimpan anomali berdasarkan ruangan (dengan RoomName sebagai key)
    public Dictionary<Anomaly.RoomName, List<Anomaly>> roomAnomalies = new Dictionary<Anomaly.RoomName, List<Anomaly>>();

    // Rentang waktu acak untuk memicu anomali (antara 5 sampai 20 detik)
    public float minAnomalyTime = 5f;
    public float maxAnomalyTime = 20f;

    private Anomaly currentAnomaly;  // Menyimpan anomali yang sedang aktif
    
    private void Start()
    {
        // Mendaftarkan semua anomali di scene
        RegisterAllAnomalies();

        // Memulai coroutine untuk memicu anomali secara acak
        StartCoroutine(RandomAnomalyTrigger());
    }

    // Fungsi untuk mendaftarkan semua anomali yang ada di scene
    private void RegisterAllAnomalies()
    {
        // Dapatkan semua objek yang memiliki script Anomaly
        Anomaly[] allAnomalies = FindObjectsOfType<Anomaly>();

        foreach (var anomaly in allAnomalies)
        {
            RegisterAnomaly(anomaly);
        }
    }

    // Fungsi untuk mendaftarkan anomali ke Dictionary berdasarkan nama ruangan
    public void RegisterAnomaly(Anomaly anomaly)
    {
        // Jika ruangan belum ada di Dictionary, tambahkan
        if (!roomAnomalies.ContainsKey(anomaly.roomName))
        {
            roomAnomalies.Add(anomaly.roomName, new List<Anomaly>());
        }

        // Tambahkan anomali ke daftar anomali di ruangan tersebut
        roomAnomalies[anomaly.roomName].Add(anomaly);
    }

    // Coroutine untuk memicu anomali secara acak setelah waktu acak antara 5-20 detik
    private IEnumerator RandomAnomalyTrigger()
    {
        while (true)
        {
            // Jika tidak ada anomali yang aktif, atau anomali aktif sudah dilaporkan
            if (currentAnomaly == null || currentAnomaly.IsReported())
            {
                // Tunggu waktu acak antara 5 hingga 20 detik sebelum memicu anomali berikutnya
                yield return new WaitForSeconds(Random.Range(minAnomalyTime, maxAnomalyTime));

                // Memilih ruangan secara acak dari Dictionary
                List<Anomaly.RoomName> roomNames = new List<Anomaly.RoomName>(roomAnomalies.Keys);
                Anomaly.RoomName randomRoom = roomNames[Random.Range(0, roomNames.Count)];

                // Memilih anomali secara acak dari ruangan tersebut
                List<Anomaly> anomaliesInRoom = roomAnomalies[randomRoom];
                currentAnomaly = anomaliesInRoom[Random.Range(0, anomaliesInRoom.Count)];

                // Memicu anomali
                currentAnomaly.TriggerAnomaly();

                Debug.Log($"Anomali '{currentAnomaly.anomalyType}' terjadi di ruangan '{currentAnomaly.roomName}'");
            }

            yield return null;
        }
    }

    // Fungsi untuk validasi laporan anomali dari pemain
    public bool ValidateReport(Anomaly.RoomName roomName, Anomaly.AnomalyType anomalyType)
    {
        // Cek apakah ruangan ada dalam Dictionary
        if (roomAnomalies.ContainsKey(roomName))
        {
            // Cari anomali dengan jenis anomalyType yang aktif di ruangan tersebut
            foreach (var anomaly in roomAnomalies[roomName])
            {
                if (anomaly.anomalyType == anomalyType && anomaly.IsTriggered())
                {
                    // Reset anomali jika ditemukan
                    anomaly.ResetAnomaly();
                    return true;
                }
            }
        }

        return false; // Jika tidak ditemukan anomali yang cocok
    }

    // Fungsi untuk memeriksa apakah ada anomali yang belum dilaporkan
    public Anomaly CheckUnreportedAnomaly()
    {
        foreach (var room in roomAnomalies.Values)
        {
            foreach (var anomaly in room)
            {
                if (anomaly.IsTriggered() && !anomaly.IsReported())
                {
                    return anomaly; // Mengembalikan anomali yang belum dilaporkan
                }
            }
        }

        return null; // Tidak ada anomali yang belum dilaporkan
    }
}
