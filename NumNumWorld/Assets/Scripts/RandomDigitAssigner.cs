using UnityEngine;
using UnityEngine.UI;

public class RandomDigitDrawer : MonoBehaviour
{
    [Header("UI Image to show the digit sprite")]
    public Image digitImage;

    [Header("Sprites for digits 1 through 9")]
    public Sprite[] digitSprites; // Should be length 9: index 0 = 1, index 8 = 9

    private int digitValue;
    private Vector3 originalPosition; // Variable to store the original position

    void Start()
    {
        // Track the original position relative to the parent (local position)
        originalPosition = transform.localPosition;

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

    // You can use this method to get the original position (in local space relative to the parent)
    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }
}
