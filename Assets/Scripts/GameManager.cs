using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string error_message = "";
    private string error_log_path = Application.dataPath + "/!Dev Info/Error Log.txt";
    private bool error_occured = false;
    private Rect centre_window_pos = new Rect(0,0,0,0);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        createTextFile(error_log_path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void error(string message)
    {
        error_message = message;
        Debug.LogError(message);
        File.AppendAllText(error_log_path, error_message + "\n");
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

    void createTextFile(string filepath)
    {
        if(!File.Exists(filepath))
        {
            File.WriteAllText(filepath,"Log Started: " + DateTime.Now.ToString() + "\n\n");
        }
        else
        {
            File.AppendAllText(filepath, "New Log: " + DateTime.Now.ToString() + "\n\n");
        }

    }

    public void fishSeen(string fish_index)
    {
        
    }
}
