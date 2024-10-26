using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSystem : MonoBehaviour{
    public GameObject spawner, menu,billing;

    float score=0;
    Transform spawnPoint;
    public PlayerMovement playerMovement; 
    public TMP_Text scoreText;
    public GameObject[] customerPrefabs;
    CustomerQueue customerQueue;
    private System.Random random = new System.Random();
    void Start (){
        spawnPoint= spawner.transform;
        customerQueue=gameObject.GetComponent<CustomerQueue>();
        StartCoroutine(SpawnCustomers());
    }

    public void SetScore(float money){
        score=score+money;
    }
     IEnumerator SpawnCustomers(){
        while (true){
            if (customerQueue.Count() < 4){
                // Spawn a random customer prefab
                SpawnCustomer();
                //Debug.Log(customerQueue.Count());
                // Wait for a random time between 5 and 10 seconds
                float waitTime = random.Next(5, 11);
                yield return new WaitForSeconds(waitTime);
            }
            else{
                // Wait a small interval to check the queue again
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private void SpawnCustomer(){
            // Select a random prefab from the array
            int randomIndex = random.Next(0, customerPrefabs.Length);
            GameObject randomPrefab = customerPrefabs[randomIndex];

            // Instantiate the selected prefab at the spawn point
            GameObject newCustomer = Instantiate(randomPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Add the spawned customer to the queue
            customerQueue.EnqueueCustomer(newCustomer);
    }
    public float sodaPrice(){
        return 1.75f;
    }
    public float beerPrice(){
        return 2.99f;
    }
    public float pastryPrice(){
        return 3.99f;
    }
    public void exitGame(){
        Application.Quit();
    }
    public void closeMenu(){
        billing.SetActive(false);
        menu.SetActive(false);
        if (playerMovement != null){
                    playerMovement.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if(scoreText!=null){
            scoreText.text ="Money Made: $"+score.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            menu.SetActive(!menu.activeSelf);
            if(menu.activeSelf){
                billing.SetActive(false);
                if (playerMovement != null){
                    playerMovement.enabled = false;
                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else{
                if (playerMovement != null){
                    playerMovement.enabled = true;
                }
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
    }
} 
 

