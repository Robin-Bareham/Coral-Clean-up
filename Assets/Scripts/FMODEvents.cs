using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{


    [field: Header("Camera SFX")]

    [field: SerializeField] public EventReference CameraShoot {  get; set; }



    public static FMODEvents instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            //TODO: Custom Error Handler Implimentation
        }
        instance = this;
    }

}
