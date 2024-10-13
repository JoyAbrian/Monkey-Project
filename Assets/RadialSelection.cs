using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;  // Pastikan menambahkan namespace untuk Input System
using UnityEngine.InputSystem.XR; // Pastikan menambahkan namespace untuk Input System XR

public class RadialSelection : MonoBehaviour
{
    // public OVRInput.Button spawnButton;
    public InputActionReference spawnAction;

    [Range(1, 10)]
    public int numberOfRadialOptions;
    public GameObject radialPartPrefab;
    public Transform radialOptionCanvas;
    public float angleBetweenRadialOptions =10;
    public Transform handTransform;

    public UnityEvent<int> onOptionSelected;

    private List<GameObject> radialOptions = new List<GameObject>();
    private int currentSelectedOption = -1;
    // Start is called before the first frame update
void Start()
{
    if (radialOptionCanvas == null)
    {
        Debug.LogError("RadialOptionCanvas is not assigned!");
    }
    if (handTransform == null)
    {
        Debug.LogError("HandTransform is not assigned!");
    }
    if (radialPartPrefab == null)
    {
        Debug.LogError("RadialPartPrefab is not assigned!");
    }
}

    // Update is called once per frame
void Update()
{
    if (radialOptionCanvas == null || handTransform == null)
    {
        Debug.LogError("RadialOptionCanvas or HandTransform is not assigned in the Inspector!");
        return;
    }

    if (spawnAction.action.WasPressedThisFrame())
    {
        Debug.Log("Trigger Pressed");
        CreateRadialSelection();
    }
    if (spawnAction.action.IsPressed())
    {
        Debug.Log("Trigger Held");
        getSelectedOption();
    }
    if (spawnAction.action.WasReleasedThisFrame())
    {
        Debug.Log("Trigger Released");
        HideAndTriggerSelected();
    }
}



    public void getSelectedOption(){
        Vector3 centerToHand = handTransform.position - radialOptionCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialOptionCanvas.forward);
        
        float angle = Vector3.SignedAngle(radialOptionCanvas.up, centerToHandProjected, radialOptionCanvas.forward);
        // float angle = Vector3.SignedAngle(radialOptionCanvas.up, centerToHandProjected, -radialOptionCanvas.forward); 
        

        if (angle < 0){
            angle += 360;
        }

        currentSelectedOption = (int) angle * numberOfRadialOptions / 360;

        for (int i = 0; i < radialOptions.Count; i++){
            if (i == currentSelectedOption){
                radialOptions[i].GetComponent<Image>().color = Color.red;
                radialOptions[i].transform.localScale = 1.1f * Vector3.one;
        }else{
            radialOptions[i].GetComponent<Image>().color = Color.white;
            radialOptions[i].transform.localScale = Vector3.one;
        }
    }
    }

    public void HideAndTriggerSelected(){


    if (onOptionSelected != null)
    {
        onOptionSelected.Invoke(currentSelectedOption);
    }
    else
    {
        Debug.LogWarning("No listeners attached to onOptionSelected");
    }
    // radialOptionCanvas.gameObject.SetActive(false);


        // onOptionSelected.Invoke(currentSelectedOption);
        radialOptionCanvas.gameObject.SetActive(false);
    }

    public void CreateRadialSelection(){

        radialOptionCanvas.gameObject.SetActive(true);
        radialOptionCanvas.position = handTransform.position;
        radialOptionCanvas.rotation = handTransform.rotation;

        foreach (var item in radialOptions)
        {
            Destroy(item);
        }

        radialOptions.Clear();


        for (int i = 0; i < numberOfRadialOptions; i++)
        {
            float angle = i *360 / numberOfRadialOptions - angleBetweenRadialOptions / 2;
            // float angle = -i *360 / numberOfRadialOptions - angleBetweenRadialOptions / 2;
            Vector3 radialOptionEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialOption = Instantiate(radialPartPrefab, radialOptionCanvas);
            spawnedRadialOption.transform.position = radialOptionCanvas.position;
            // spawnedRadialOption.transform.rotate = radialOptionCanvas.rotate;
            spawnedRadialOption.transform.localEulerAngles = radialOptionEulerAngle;

            spawnedRadialOption.GetComponent<Image>().fillAmount = (1f / (float)numberOfRadialOptions) - (angleBetweenRadialOptions /360);

            radialOptions.Add(spawnedRadialOption);
        }
    }
}
