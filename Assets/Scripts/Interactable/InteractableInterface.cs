using UnityEngine;

public interface InteractableInterface
{
    void OnInteract();

}

// Interactable objects need this interface to be implemented.
// The reycast calls OnInteract() when it picks up your object.