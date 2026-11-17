using UnityEngine;

public class CollectableInformation : MonoBehaviour
{
    

    // The purpose of this script is to contain information about items that the player can pick up.
    // Attach it to objects that are meant to be trash.

    // On its own, this script does nothing.

    public string itemName;
    public string itemDescription;
    

    // Item Indentifiers.
    // ONLY EVER ENABLE ONE AT A TIME.
    public bool isTrash;
    public bool isCoral;
    public bool isOther;




    public void onPickup()
    { 
        UnityEngine.Debug.Log("Picked up: " + itemName);
        Destroy(gameObject);
    }
}
