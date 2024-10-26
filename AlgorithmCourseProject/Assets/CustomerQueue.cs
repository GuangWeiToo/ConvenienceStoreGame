using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    private Queue<GameObject> customerQueue = new Queue<GameObject>();
    float playerTotal;
    float customerTotal;
    GameObject checkout;
    RegisterCheckOut checkScript;
    void Start()
    {
        checkout= GameObject.Find("Main Camera");
        checkScript= checkout.GetComponent<RegisterCheckOut>();
    }

    public void EnqueueCustomer(GameObject customer){
        customerQueue.Enqueue(customer);
    }
    public void DequeueCustomer(){
        customerQueue.Dequeue();
    }
    public int Count(){
        return customerQueue.Count;
    }
    void Update()
    {
        //if there are objects in queue
        if(customerQueue.Count>0){
            //get object data total and name
            GameObject customerObj=customerQueue.Peek();
            Customer customerScript=customerObj.GetComponent<Customer>();
            customerScript.moveTo("cashier");
            customerTotal=customerScript.getTotal();
            playerTotal=checkScript.getTotal();
            if((playerTotal!=0)&&(customerTotal!=0)&&(playerTotal==customerTotal)){
                GameObject system=GameObject.Find("System");
                system.GetComponent<GameSystem>().SetScore(customerTotal);
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                //dequeue
                customerQueue.Dequeue();
                customerScript.Leave();
            }
        }
    }
}
