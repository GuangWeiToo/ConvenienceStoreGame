using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HoverTextInteraction : MonoBehaviour
{
    public GameObject targetObject;   
    public TMP_Text hoverText;            
    public string message; 
    
    void Start()
    {
        hoverText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == targetObject)
            {
                hoverText.gameObject.SetActive(true);
                hoverText.text = message;
            }
            else
            {
                hoverText.gameObject.SetActive(false);
            }
        }
        else
        {
            hoverText.gameObject.SetActive(false);
        }
    }
}
