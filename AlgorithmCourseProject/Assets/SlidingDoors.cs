using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : MonoBehaviour{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public float slideDistance = 2.0f;     // Distance for each door to slide
    public float slideSpeed = 2.0f;        // Speed of door sliding
    
    private Vector3 leftDoorClosedPos;
    private Vector3 rightDoorClosedPos;
    private bool isOpen = false;

    void Start(){
        // Store initial closed positions for the doors
        leftDoorClosedPos = leftDoor.transform.position;
        rightDoorClosedPos = rightDoor.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen){
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            StartCoroutine(SlideDoorsOpen());
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if (isOpen){
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            StartCoroutine(SlideDoorsClosed());
            isOpen = false;
        }
    }

    private IEnumerator SlideDoorsOpen(){
        Vector3 leftDoorTargetPos = leftDoorClosedPos - new Vector3(slideDistance, 0, 0);
        Vector3 rightDoorTargetPos = rightDoorClosedPos + new Vector3(slideDistance, 0, 0);

        while (Vector3.Distance(leftDoor.transform.position, leftDoorTargetPos) > 0.01f){
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, leftDoorTargetPos, slideSpeed * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, rightDoorTargetPos, slideSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator SlideDoorsClosed(){
        while (Vector3.Distance(leftDoor.transform.position, leftDoorClosedPos) > 0.01f){
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, leftDoorClosedPos, slideSpeed * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, rightDoorClosedPos, slideSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
