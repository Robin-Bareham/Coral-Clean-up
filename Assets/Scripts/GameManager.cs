using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject errorCanvas;
    [SerializeField] TMP_Text errorBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void error(string message)
    {
        Debug.LogError(message);
        //errorBody.text = message;
        //errorCanvas.SetActive(true);

        GUI.Window(0, new Rect(Screen.width - 200, Screen.height - 100, 400, 200), windowFunction, message);
    }

    void windowFunction(int windowID)
    {
        if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        {
            print("Got a click");
        }
    }

    public void fishSeen(string fish_index)
    {
        
    }
}
