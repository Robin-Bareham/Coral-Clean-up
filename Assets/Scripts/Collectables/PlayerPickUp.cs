using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;  
public class PlayerPickUp : MonoBehaviour
{



    [SerializeField]
    private InputActionAsset PlayerControls;

    [SerializeField]
    private float raycastDistance = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControls.FindActionMap("Player").FindAction("Pickup").triggered)
        {
            performRaycast();
        }

      
        


    }

    void performRaycast() 
    {
        debugRay();

        for (int i = 0; i < 10; i++)
        {
            print("Raycast performed");
        }
        

        

    }

    void debugRay()
    {
        UnityEngine.Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red, 1f);
    }





}
