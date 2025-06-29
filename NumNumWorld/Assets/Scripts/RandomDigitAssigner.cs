using UnityEngine;
using UnityEngine.UI;

public class RandomDigitDrawer : MonoBehaviour
{
    [Header("UI Image to show the digit sprite")]
    public Image digitImage;

    [Header("Sprites for digits 1 through 9")]
    public Sprite[] digitSprites; // Should be length 9: index 0 = 1, index 8 = 9

    private int digitValue;

    private Vector3 originalLocalPosition;     // Track original local position
    private Transform originalParent;          // Track original parent transform

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        originalParent = transform.parent;

        DrawRandomDigit();
    }

    public void DrawRandomDigit()
    {
        int index = Random.Range(0, digitSprites.Length); // 0 to 8
        digitImage.sprite = digitSprites[index];
        digitValue = index + 1;
    }

    public int GetDigitValue()
    {
        return digitValue;
    }

    public Vector3 GetOriginalPosition()
    {
        return originalLocalPosition;
    }

    public void ResetToOriginalPosition()
    {
        transform.SetParent(originalParent);
        transform.localPosition = originalLocalPosition;

        gameObject.SetActive(true); // Ensure it's visible
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
    }
}
