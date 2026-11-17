using UnityEngine;

public class CoralInteractable : MonoBehaviour, InteractableInterface
{
    
    Material coralMaterial;


    [SerializeField]
    coralState currentCoralState = coralState.unset;
    enum coralState
    {
        dead,
        alive,
        unset
    }



    void Awake()
    {
        coralMaterial = GetComponent<Renderer>().material;

        // Handle the "Unset" logic during initialization
        if (currentCoralState == coralState.unset)
        {
            currentCoralState = coralState.dead;
            coralMaterial.color = new Color32(175, 125, 175, 255);
        }
    }

    public void OnInteract() 
    {
        if (currentCoralState == coralState.dead) 
        {
            coralMaterial.color = new Color32(200, 0, 200, 255);
            currentCoralState = coralState.alive;
            Debug.Log("Coral has been rejuvinated!");
        }
        else if (currentCoralState == coralState.alive)
        {
                Debug.Log("Coral has already been rejuvinated.");
        }
        else 
        {
            Debug.Log("ERROR: Coral in undefined state");

        }
    }
}
