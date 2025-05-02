using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DigitDropSlot : MonoBehaviour, IDropHandler
{
    public Image slotImage;
    public Sprite defaultSprite;  // Assign this in the Inspector
    public int lockedInValue = 0;
    private GameObject currentlyAssignedDigit;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        RandomDigitDrawer digit = dropped.GetComponent<RandomDigitDrawer>();
        Image droppedImage = dropped.GetComponent<Image>();

        if (digit != null && droppedImage != null)
        {
            // If there's already a digit here, reset it before placing the new one
            if (currentlyAssignedDigit != null)
            {
                ResetAssignedDigit(); // <- this ensures the current one is put back
            }

            // Assign new digit
            slotImage.sprite = droppedImage.sprite;
            slotImage.color = Color.white;
            lockedInValue = digit.GetDigitValue();
            currentlyAssignedDigit = dropped;

            // Deactivate the dropped digit and make it non-interactable
            dropped.SetActive(false);
            CanvasGroup canvasGroup = dropped.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            Debug.Log("Locked in value: " + lockedInValue);
        }
    }

    private void ResetAssignedDigit()
    {
        if (currentlyAssignedDigit != null)
        {
            RandomDigitDrawer originalDigit = currentlyAssignedDigit.GetComponent<RandomDigitDrawer>();
            Vector3 originalLocalPosition = originalDigit.GetOriginalPosition();

            currentlyAssignedDigit.SetActive(true);
            currentlyAssignedDigit.transform.SetParent(originalDigit.transform.parent);
            currentlyAssignedDigit.transform.localPosition = originalLocalPosition;

            CanvasGroup canvasGroup = currentlyAssignedDigit.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;

            currentlyAssignedDigit = null;
        }
    }

    public void ResetSlot()
    {
        ResetAssignedDigit();
        slotImage.sprite = defaultSprite;
        slotImage.color = Color.white;
        lockedInValue = 0;
    }
}
