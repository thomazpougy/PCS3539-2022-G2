using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreArea : MonoBehaviour
{
    // public GameObject effectObject;
    public GameObject pointsTxt;

    private int points = 0;

    void OnTriggerEnter(Collider otherCollider){
        if (otherCollider.GetComponent<Rigidbody>()!=null){
            // effectObject.SetActive(true);
            Debug.Log("Score!");
            points += 1;
            pointsTxt.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
            
        }
    }
}
