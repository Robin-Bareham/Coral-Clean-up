using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string error_message = "";
    private bool error_occured = false;
    private Rect centre_window_pos = new Rect(0,0,0,0);


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
        error_message = message;
        Debug.LogError(message);
        error_occured = true;        
    }

    private void OnGUI()
    {     
        if (error_occured) 
        {
            centre_window_pos = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
            GUI.Window(0, centre_window_pos, errorWindow, "Something Fishy Happened");
        }
        
    }

    void errorWindow(int windowID)
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(0, 0, 400, 150), error_message, centeredStyle);
        
        if(GUI.Button( new Rect((centre_window_pos.width/2) - 75, centre_window_pos.height - 60, 70, 50), "Close"))
        {
            error_occured = false;
        }
        else if(GUI.Button(new Rect (centre_window_pos.width/2 + 5, centre_window_pos.height - 60, 70, 50), "Quit"))
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void fishSeen(string fish_index)
    {
        
    }
}
