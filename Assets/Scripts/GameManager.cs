using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private string error_message = "";
    private string error_log_path = Application.dataPath + "/!Dev Info/Error Log.txt";
    private bool error_occured = false;
    private Rect centre_window_pos = new Rect(0,0,0,0);


    private int photo_count = 0;
    private int coral_count = 1;
    private int trash_count = 0;
    private int current_day = 0;

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

    //Function called upon starting day / after going to sleep
    //This function will require expanding on as time goes on and more functions are added to the game.
    public void nextDay()
    {
        //Reset oxygen counter
        //Increase day counter
        current_day += 1;

        //Spawn fish
        //Grow Coral
        //Dirty/Clean areas.
    }


    //GET SET FUNCTIONS FOR THE COUNTERS
    public int getPhotoCount()
    {
        return photo_count;
    }

    public void changePhotoCount(int new_num)
    {
        photo_count = new_num;
    }

    public int getCoralCount()
    {
        return coral_count;
    }

    public void changeCoralCount(int new_num) 
    {
        coral_count = new_num;
    }

    public int getTrashCount()
    {
        return trash_count;
    }

    public void changeTrashCount(int new_num)
    {
        trash_count = new_num;
    }

    public int getDay()
    {
        return current_day;
    }
}
