using UnityEngine;

public class GenericInteractable : MonoBehaviour, InteractableInterface
{
    // Example of ainteractable object. Contains the interable interface and OnInteract() function.
    // OnInteract() is called when the player interacts with the object.

    public void OnInteract()
    {
        Material coralMaterial = GetComponent<Renderer>().material;
        coralMaterial.color = new Color(Random.value, Random.value, Random.value);

    }
}
