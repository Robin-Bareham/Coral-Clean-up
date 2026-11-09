using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Journal : MonoBehaviour
{
    GameManager manager;

    private string[] fish_types = { }; //Append to array when adding new fish type
    private int[] fish_tracker = { }; //Append new 0 when adding new fish type

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();

        if(fish_types.Length != fish_tracker.Length)
        {
            manager.error("Error 002: Fish types and tracker are not equal.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void fishSeen(string fish_index)
    {
        for (int i = 0; i < fish_types.Length; i++)
        {
            if (fish_types[i] == fish_index)
            {
                fish_tracker[i]++;
                break;
            }
            else if (i + 1 > fish_types.Length)
            {
                //error: Fish not found
                manager.error("Error 001: Fish type " +  fish_index + " not valid");
            }
        }
        //if fish_tracker[i] == 0, the page is locked. If fish_tracker[i] > 0, the page is unlocked. This line increments that value when the fish is photographed.
    }

    void journalUpdate()
    {
        for (int i = 0; i < fish_types.Length; i++)
        {
            if (fish_tracker[i] < 0)
            {
                //Update text from "???" to fish name
                //Clicking name in journal opens the right page at level=fish_tracker[i]
            }
        }
    }
}
