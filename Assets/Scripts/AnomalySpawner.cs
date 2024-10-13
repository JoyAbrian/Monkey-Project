using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalySpawner : MonoBehaviour
{
    private List<GameObject> children = new List<GameObject>();
    public float activationDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
            // Disable all children initially
            child.gameObject.SetActive(false);
        }
        StartCoroutine(ActivateRandomChild());
    }
    IEnumerator ActivateRandomChild()
    {
        while (true)
        {
            // Deactivate all children
            foreach (GameObject child in children)
            {
                child.SetActive(false);
            }

            // Choose a random child and activate it
            int randomIndex = Random.Range(0, children.Count);
            children[randomIndex].SetActive(true);

            // Wait for the specified delay before the next activation
            yield return new WaitForSeconds(activationDelay);
        }
    }
}
