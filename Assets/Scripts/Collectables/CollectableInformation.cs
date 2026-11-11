using UnityEngine;

public class CollectableInformation : MonoBehaviour
{
    

    // The purpose of this script is to contain information about items that the player can pick up.
    // Attach it to objects that are meant to be trash.

    // On its own, this script does nothing.

    public string itemName;
    public string itemDescription;

    
    public void onPickup()
    { 
        UnityEngine.Debug.Log("Picked up: " + itemName);
        Destroy(gameObject);
    }
}
