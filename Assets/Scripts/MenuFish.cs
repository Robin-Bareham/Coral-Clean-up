using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float swimSpeed = 100f;
    [SerializeField] private bool swimRight = true;
    [SerializeField] private float bobHeight = 10f;
    [SerializeField] private float bobSpeed = 2f;


    private RectTransform rectTransform;
    private float startY;
    private float randomOffset;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;

        // fish starting rhythm, so they don't bob in sync
        randomOffset = Random.Range(0f, 10f);
    }

    private void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;

        // horizontal motion
        float direction = swimRight ? 1f : -1f;
        pos.x += swimSpeed * direction * Time.deltaTime;

        // vertical motion
        pos.y = startY + Mathf.Sin((Time.time + randomOffset) * bobSpeed) * bobHeight;

        rectTransform.anchoredPosition = pos;
    }
}