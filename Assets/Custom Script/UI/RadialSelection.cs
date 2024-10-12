using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadialSelection : MonoBehaviour
{
    [Range(2, 10)] public int numberOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetwenRadialPart = 10;
    public Transform handTransform;

    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GenerateRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = - i * 360 / numberOfRadialPart - angleBetwenRadialPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject radialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            radialPart.transform.position = radialPartCanvas.position;
            radialPart.transform.localEulerAngles = radialPartEulerAngle;

            radialPart.GetComponent<Image>().fillAmount = (1 / (float) numberOfRadialPart) - (angleBetwenRadialPart/360);

            spawnedParts.Add(radialPart);
        }
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if (angle < 0)
        {
            angle += 360;
        }

        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if (i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            } 
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }
}
