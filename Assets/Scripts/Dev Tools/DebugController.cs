using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool show_console;
    bool show_help;

    string input;

    // add new 'public static DebugCommand name' here whenever a new debug command is added
    // use 'public static DebugCommand<DATA_TYPE> name' if values needs to be included
    public static DebugCommand help;
    public static DebugCommand quit;
    public List<object> commandList;

    Vector2 scroll;

    public void OnToggleDebug(InputValue value)
    {
        show_console = !show_console;

    }

    public void OnReturn(InputValue value)
    {
        if(show_console)
        {
            HandleInput();
            input = "";
        }
    }

    private void Awake()
    {
        //instance of new debug command
        //arguments: (Command_Name, Command Description, format_for_command_input <value_name_if_needed>, (arguments) => define function here { });
        quit = new DebugCommand("quit", "Quits the game", "/quit", () =>
        {
            //quit the game
            Application.Quit();
            Debug.Log("Quit");
        });

        help = new DebugCommand("help", "Displays all debug commands in a list", "/help", () => 
        {
            show_help = true;
        });

        commandList = new List<object>
        {
            //List of command names
            //Please alphabetise
            help,
            quit,
        };
    }

    private void OnGUI()
    {
        if(!show_console)
        {
            return;
        }

        float y = 0f;
        float history_length = 100;
        float line_space = 30;
        float input_line_top = 10;
        float padding = 5;
        float line_height = 20;

        if(show_help)
        {
            GUI.Box(new Rect(0, y, Screen.width, history_length), "");

            Rect viewport = new Rect(0, 0, Screen.width - line_space, line_height * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + padding, Screen.width, history_length - input_line_top), scroll, viewport);

            for(int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(padding, line_height * i, viewport.width - history_length, line_height);
                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y += history_length;
        }

        GUI.Box(new Rect(0, y, Screen.width, line_space), "");
        GUI.backgroundColor = new Color(0,0,0,0);
        GUI.SetNextControlName("console");
        input = GUI.TextField(new Rect(input_line_top, y + padding, Screen.width - line_height, line_height), input);
        GUI.FocusControl("console");
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for (int i=0; i<commandList.Count;i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandID))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                /* Template for other data types || create new uncommented 'else' whenever a new data type will be used as debug option
                else if (commandList[i] as DebugCommand<DATA_TYPE> != null)
                {
                    (commandList[i] as DebugCommand<DATA_TYPE>).Invoke(DATA_TYPE.Parse(properties[1]));
                }
                */ 
            }

        }
    }

}
