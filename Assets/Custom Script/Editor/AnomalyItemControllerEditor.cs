using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnomalyItemController))]
public class AnomalyItemControllerEditor : Editor {
    public override void OnInspectorGUI()
    {
        // Mendapatkan referensi ke target (AnomalyItemController)
        AnomalyItemController controller = (AnomalyItemController)target;

        // Menampilkan Anomaly Type
        controller.anomalyType = (Anomaly.AnomalyType)EditorGUILayout.EnumPopup("Anomaly Type", controller.anomalyType);

        // Switch case untuk mengatur field yang aktif tergantung pada tipe anomali
        switch (controller.anomalyType)
        {
            case Anomaly.AnomalyType.Light:
                EditorGUILayout.LabelField("Light Settings", EditorStyles.boldLabel);
                controller.targetLight = (Light)EditorGUILayout.ObjectField("Target Light", controller.targetLight, typeof(Light), true);
                break;

            case Anomaly.AnomalyType.Ghost:
                EditorGUILayout.LabelField("Ghost Settings", EditorStyles.boldLabel);
                controller.ghostPrefab = (GameObject)EditorGUILayout.ObjectField("Ghost Prefab", controller.ghostPrefab, typeof(GameObject), true);
                SerializedProperty ghostPositions = serializedObject.FindProperty("ghostPositions");
                EditorGUILayout.PropertyField(ghostPositions, true);  // Menampilkan list posisi Ghost
                break;

            case Anomaly.AnomalyType.MovingObject:
                EditorGUILayout.LabelField("Moving Object Settings", EditorStyles.boldLabel);
                controller.anomalyPosition = EditorGUILayout.Vector3Field("Anomaly Position", controller.anomalyPosition);
                controller.anomalyRotation = EditorGUILayout.Vector3Field("Anomaly Rotation", controller.anomalyRotation);
                break;

            case Anomaly.AnomalyType.MissingObject:
                EditorGUILayout.LabelField("Missing Object Settings", EditorStyles.boldLabel);
                controller.isVisibleDuringAnomaly = EditorGUILayout.Toggle("Visible During Anomaly", controller.isVisibleDuringAnomaly);
                break;

            default:
                break;
        }

        // Jangan lupa apply perubahan serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
