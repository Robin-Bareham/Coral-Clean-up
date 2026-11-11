using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

///
/// SAMUAL SCRIPT: If you are reading this and dont understand something,
/// let me know.
/// 
/// ALSO: This script could be modified to become a generic interaction script,
/// might be a wise call later on to avoid too many scripts.
///
public class PlayerPickUp : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset PlayerControls;

    [SerializeField]
    float raycastDistance = 10f; // customizable in engine, default value

    // Using input system message call
    void OnPickUp()
    { 
        performRaycast();
    }

    void performRaycast() 
    {
        
        debugRay(); // Does nothing expect draw a ray in engine.
        // Sidenote: This debug ray is not the same as the actual raycast we do later. ONLY use this to check control are actually running.
        
        Ray raycast = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Black magic, starting point is center of the players screen. 
        RaycastHit rayhit;
        UnityEngine.Debug.Log("Raycast performed");

        if (Physics.Raycast(raycast, out rayhit, raycastDistance))
        {
            CollectableInformation trash = rayhit.collider.GetComponent<CollectableInformation>();
            if (trash != null)
            {
                UnityEngine.Debug.Log("Trash found: " + trash.itemName + ", Attempting Pickup");
                // Inbertween confiration of pickup, we can grab information about the object before we destroy it via onPickup(),
                // Will be used later on.
                trash.onPickup();
            }
            else 
            { 
                UnityEngine.Debug.Log("No trash found");
            }
        }
    }

    void debugRay()
    {
        UnityEngine.Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red, 1f);
    }
}
