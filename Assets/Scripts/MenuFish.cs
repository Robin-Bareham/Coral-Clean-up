using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float swimSpeed = 150f;
    [SerializeField] private bool swimRight = true;
    [SerializeField] private bool spriteFacesLeftByDefault = false;
    [SerializeField] private float bobHeight = 10f;
    [SerializeField] private float bobSpeed = 2f;

    [Header("Turnaround Edge")]
    [SerializeField] private float screenEdgeX = 1050f;

    private RectTransform rectTransform;
    private float startY;
    private float randomOffset;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;

        // fish starting rhythm, so they don't bob in sync
        randomOffset = Random.Range(0f, 10f);

        UpdateFacingDirection();
    }

    private void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;

        // horizontal motion
        float direction = swimRight ? 1f : -1f;
        pos.x += swimSpeed * direction * Time.deltaTime;

        // vertical motion
        pos.y = startY + Mathf.Sin((Time.time + randomOffset) * bobSpeed) * bobHeight;

        // turn around slightly off-screen
        if (swimRight && pos.x >= screenEdgeX)
        {
            pos.x = screenEdgeX;
            swimRight = false;
            UpdateFacingDirection();
        }
        else if (!swimRight && pos.x <= -screenEdgeX)
        {
            pos.x = -screenEdgeX;
            swimRight = true;
            UpdateFacingDirection();
        }

        rectTransform.anchoredPosition = pos;
    }

    private void UpdateFacingDirection()
    {
        Vector3 scale = rectTransform.localScale;
        float absX = Mathf.Abs(scale.x);

        bool shouldFlip = spriteFacesLeftByDefault ? swimRight : !swimRight;
        scale.x = shouldFlip ? -absX : absX;

        rectTransform.localScale = scale;
    }
}