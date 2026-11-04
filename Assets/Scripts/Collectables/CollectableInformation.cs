using UnityEngine;

public class CollectableInformation : MonoBehaviour
{
    // Currently this script does not contrain a way to send its information to another script.
    // this functionality will be added later.

    // PURPOSE: Contains information about items within the world.
    // 




    public string itemName;
    public string itemDescription;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onPickup()
    { 
        UnityEngine.Debug.Log("Picked up: " + itemName);
        Destroy(gameObject);
    }
}
