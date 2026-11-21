using UnityEngine;

public class Fish : MonoBehaviour
{
    GameManager manager;
    [SerializeField] string type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fishSeen()
    {
        manager.fishSeen(type);
    }
}
