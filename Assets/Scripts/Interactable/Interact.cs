using System.Diagnostics;
using UnityEditor.Analytics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

///
/// SAMUAL SCRIPT: If you are reading this and dont understand something,
/// let me know.
/// 
/// ALSO: This script could be modified to become a generic interaction script,
/// might be a wise call later on to avoid too many scripts.
///
public class Interact : MonoBehaviour
{
    GameManager manager;
    [SerializeField]
    private InputActionAsset PlayerControls;

    [SerializeField]
    float raycastDistance = 10f; // customizable in engine, default value

    int amountOfTrashCollected = 0;
    int amountOfCoralCollected = 0;






    public int getamountOfTrashCollecter() 
    {
        return amountOfTrashCollected;
    }
    public void setamountOfTrashCollected(int i) 
    {
        amountOfTrashCollected = i;
    }

    public int getamountOfCoralCollecter()
    {
        return amountOfCoralCollected;
    }
    public void setamountOfCoralCollected(int i)
    {
        amountOfCoralCollected = i;
    }




    // Using input system message call
    void OnInteract()
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
            CollectableInformation collectable = rayhit.collider.GetComponent<CollectableInformation>();
            InteractableInterface interactable = rayhit.collider.GetComponent<InteractableInterface>();
            if (collectable != null)
            {
                UnityEngine.Debug.Log("Collectable found: " + collectable.itemName + ", Attempting Pickup");
                // Inbertween confiration of pickup, we can grab information about the object before we destroy it via onPickup(),
                // Will be used later on.
                
                if (collectable.isTrash)
                {
                    collectable.onPickup();
                    setamountOfCoralCollected(getamountOfCoralCollecter() + 1);
                    UnityEngine.Debug.Log("Trash collected: " + getamountOfTrashCollecter());
                }
                else if (collectable.isCoral)
                {
                    collectable.onPickup();
                    setamountOfCoralCollected(getamountOfCoralCollecter() + 1);
                    UnityEngine.Debug.Log("Coral collected: " + getamountOfCoralCollecter());
                }
                else if (collectable.isOther)
                {
                    collectable.onPickup();
                }
                else 
                {
                    //UnityEngine.Debug.Log("ERROR, Collectable item not tagged");
                    manager.error("Error 101: Collectable item not tagged");
                }
                
            }
            else if (interactable != null)
            {
                UnityEngine.Debug.Log("Interactable found, Attempting Interaction");
                interactable.OnInteract();
            }

            else
            {
                UnityEngine.Debug.Log("No item found from raytrace");
            }
            
        }
    }

    void debugRay()
    {
        UnityEngine.Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red, 1f);
    }
}
