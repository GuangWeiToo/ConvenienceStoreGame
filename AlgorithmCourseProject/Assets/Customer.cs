using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Include NavMeshAgent

public class Customer : MonoBehaviour
{
    float myTotal, sodaPrice, beerPrice, pastryPrice;
    int numSoda, numBeer, numPastry;
    GameSystem gameSystem;
    CustomerQueue customerQueue;
    LineOcuppiedChecker occupied;
    NavMeshAgent agent;

    void Start()
    {
        // Get NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        GameObject cashier = GameObject.Find("cashier");
        occupied = cashier.GetComponent<LineOcuppiedChecker>();
        GameObject game = GameObject.Find("System");
        gameSystem = game.GetComponent<GameSystem>();
        customerQueue = game.GetComponent<CustomerQueue>();

        sodaPrice = gameSystem.sodaPrice();
        beerPrice = gameSystem.beerPrice();
        pastryPrice = gameSystem.pastryPrice();
        
        Enter();
    }

    void Enter()
    {
        StartCoroutine(MoveToRandomLocations());
        numSoda = Random.Range(0, 4);
        numBeer = Random.Range(0, 4);
        numPastry = Random.Range(0, 4);
        myTotal = (numSoda * sodaPrice) + (numBeer * beerPrice) + (6*numPastry * pastryPrice);
        customerQueue.EnqueueCustomer(gameObject);
    }

    public float getTotal()
    {
        return myTotal;
    }
    public int getSodaNum()
    {
        return numSoda;
    }
    public int getBeerNum()
    {
        return numBeer;
    }
    public int getPastryNum()
    {
        return numPastry;
    }

    public void Leave()
    {
        foreach (GameObject item in occupied.itemList)
        {
            item.SetActive(false);
        }
        moveTo("exit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "exit")
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveToRandomLocations()
    {
        string[] locations = new string[] { "magazine", "isle", "pastry", "drinks" };
        moveTo(locations[Random.Range(0, 3)]);
        yield return new WaitForSeconds(Random.Range(0, 3));
        moveTo(locations[Random.Range(0, 3)]);
    }

    public void moveTo(string newLocation)
    {
        GameObject location = GameObject.Find(newLocation);
        if (location != null){
            //NavMeshAgent to move to the target location
            agent.SetDestination(location.transform.position);
        }
    }
}
