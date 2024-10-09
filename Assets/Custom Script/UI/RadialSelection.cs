using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialSelection : MonoBehaviour
{
    [Range(2, 10)] public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetwenRadialPart = 10;

    private List<GameObject> spawnedParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRadialPart();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateRadialPart();
    }

    public void GenerateRadialPart()
    {
        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = i * 360 / numberOfRadialPart - angleBetwenRadialPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject radialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            radialPart.transform.position = radialPartCanvas.position;
            radialPart.transform.localEulerAngles = radialPartEulerAngle;

            radialPart.GetComponent<Image>().fillAmount = (1 / (float) numberOfRadialPart) - (angleBetwenRadialPart/360);

            spawnedParts.Add(radialPart);
        }
    }
}
