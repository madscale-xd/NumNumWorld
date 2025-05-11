using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DigitDropSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image slotImage;
    public Sprite defaultSprite;  // Assign in Inspector
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
            // Reset existing digit if slot is occupied
            if (currentlyAssignedDigit != null)
            {
                ResetAssignedDigit();
            }

            // Assign new digit
            slotImage.sprite = droppedImage.sprite;
            slotImage.color = Color.white;
            lockedInValue = digit.GetDigitValue();
            currentlyAssignedDigit = dropped;

            // Deactivate digit and make non-interactable
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

    // Tap to reset the slot (works on Android)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentlyAssignedDigit != null)
        {
            ResetSlot();
            Debug.Log("Slot reset by tap.");
        }
    }
}
