using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOcuppiedChecker : MonoBehaviour
{
    public List<GameObject> itemList;
    void Start(){
        itemList = new List<GameObject>();
        GameObject items = GameObject.Find("itemToggle");
        foreach (Transform item in items.transform)
        {
            itemList.Add(item.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Customer")){
            Customer cusScript=other.gameObject.GetComponent<Customer>();
            for (int i = 0; i < cusScript.getSodaNum(); i++){
                // Activate or deactivate the soda items
                GameObject sodaItem = itemList.Find(item => item.name == "soda" + i);
                if (sodaItem != null){
                    sodaItem.SetActive(true); // Set to active if the soda exists
                }
            }

            for (int i = 0; i < cusScript.getBeerNum(); i++){
                // Activate or deactivate the beer items
                GameObject beerItem = itemList.Find(item => item.name == "beer" + i);
                if (beerItem != null){
                    beerItem.SetActive(true); // Set to active if the beer exists
                }
            }

            for (int i = 0; i < cusScript.getPastryNum(); i++){
                // Activate or deactivate the pastry items
                GameObject pastryItem = itemList.Find(item => item.name == "pastry" + i);
                if (pastryItem != null){
                    pastryItem.SetActive(true); // Set to active if the pastry exists
                }
            }
        }
    }
}
