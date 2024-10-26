using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class RegisterCheckOut : MonoBehaviour
{
    public GameObject Billing;
    public GameObject Register;
    public TMP_Text hoverText; 
    public HoverTextInteraction hoverTextScript;
    public PlayerMovement playerMovement; 
    public TMP_InputField sodaInput; 
    public TMP_InputField beerInput; 
    public TMP_InputField pastryInput; 

    GameSystem gameSystem;
    float total;
    int numSoda,numBeer,numPastry;

    void Start(){
        GameObject game=GameObject.Find("System");
        gameSystem= game.GetComponent<GameSystem>();
    

        sodaInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        beerInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        pastryInput.contentType = TMP_InputField.ContentType.IntegerNumber;
        
        sodaInput.onValueChanged.AddListener(delegate { OnInputChanged(); });
        beerInput.onValueChanged.AddListener(delegate { OnInputChanged(); });
        pastryInput.onValueChanged.AddListener(delegate { OnInputChanged(); });

        sodaInput.onValueChanged.AddListener(delegate { ValidateIntegerInput(sodaInput); });
        beerInput.onValueChanged.AddListener(delegate { ValidateIntegerInput(beerInput); });
        pastryInput.onValueChanged.AddListener(delegate { ValidateIntegerInput(pastryInput); });
    }
    void OnInputChanged(){
        if (!string.IsNullOrEmpty(sodaInput.text)){
            numSoda = int.Parse(sodaInput.text);
        }
        if (!string.IsNullOrEmpty(beerInput.text)){
            numBeer = int.Parse(beerInput.text);
        }
        if (!string.IsNullOrEmpty(pastryInput.text)){
            numPastry = int.Parse(pastryInput.text);
        }
    }
    void ValidateIntegerInput(TMP_InputField inputField)
    {
        int parsedValue;
        if (!int.TryParse(inputField.text, out parsedValue))
        {
            inputField.text ="0";
        }
    }
    void EnableUIInteraction(){
        // Disable the player's movement script
        if (playerMovement != null){
            playerMovement.enabled = false;
        }
        if (hoverTextScript != null){
            hoverText.enabled = false;
        }
        if (hoverText != null){
            hoverText.enabled = false;
        }

        // Show and unlock the cursor so the player can use it to interact with the UI
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void DisableUIInteraction(){
        // Re-enable the player's movement script
        if (playerMovement != null){
            playerMovement.enabled = true;
        }
        if (hoverTextScript != null){
            hoverText.enabled = true;
        }
        if (hoverText != null){
            hoverText.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Close(){
        Billing.SetActive(false);
        DisableUIInteraction();
    }
    public void CheckOut(){
        float sodaPrice= gameSystem.sodaPrice();
        float beerPrice= gameSystem.beerPrice();
        float pastryPrice= gameSystem.pastryPrice();
        total=(sodaPrice*numSoda)+(beerPrice*numBeer)+(pastryPrice*numPastry);
        Debug.Log("PlayerTotal:"+total);
    }
    public float getTotal(){
        return total;
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)){
                if (hit.collider.gameObject == Register){
                    Billing.SetActive(!Billing.activeSelf);
                    if (Billing.activeSelf){
                        EnableUIInteraction();
                    }
                    else{
                        DisableUIInteraction();
                    }
                }
            }
        }
    }
}
