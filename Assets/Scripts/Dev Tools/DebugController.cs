using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool show_console;

    public void OnToggleDebug(InputValue value)
    {
        show_console = !show_console;

    }

    private void OnGUI()
    {
        if(!show_console)
        {
            return;
        }

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
    }
}
