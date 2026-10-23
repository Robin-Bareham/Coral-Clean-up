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
        errorBody.text = message;
        errorCanvas.SetActive(true);
    }

    public void fishSeen(string fish_index)
    {
        
    }
}
