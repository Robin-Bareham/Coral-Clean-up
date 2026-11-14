using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;
public class PhotoCameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset PlayerControls;

    [SerializeField]
    private GameObject camera_text;

    private InputAction takePhotoAction;
    private InputAction openCameraAction;
    private bool photoCameraActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    { 
        InputActionMap map = PlayerControls.FindActionMap("Player");
        //Links up action to input actions map
        takePhotoAction = map.FindAction("TakePhoto");
        openCameraAction = map.FindAction("OpenCamera");

        camera_text.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        takePhotoAction.Enable();
        openCameraAction.Enable();
    }

    private void OnDisable()
    {
        takePhotoAction.Disable();
        openCameraAction.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        //WasPressedThisFrame does a single activation for triggers. 
        if (takePhotoAction.WasPressedThisFrame() && photoCameraActive)
        {
            TakeAPhoto();
        }
        if (openCameraAction.WasPressedThisFrame())
        {
            photoCameraActive = !photoCameraActive;
            //If the camera is active, the text will appear
            camera_text.gameObject.SetActive(photoCameraActive);
        }
        //Idealy something here to say, photo taken? Show it then hide it.
    }

    private void TakeAPhoto()
    {
        //Hides UI
        camera_text.gameObject.SetActive(false);
        Debug.Log("TAKING A PHOTO");
        //Adds the time to the string, having to replace / spaces and : as they're not accepted in file names
        string date = System.DateTime.Now.ToString();
        date = date.Replace("/", "-");
        date = date.Replace(" ", "_");
        date = date.Replace(":", "-");
        //Takes the screenshot of the sceen.
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Screenshots/trials_" + date + ".png");
        //UnityEditor.AssetDatabase.Refresh();
        //Sets it to false to avoid multiple pictures at once + camera has been 'put down'
        photoCameraActive = false; 
    }
}
