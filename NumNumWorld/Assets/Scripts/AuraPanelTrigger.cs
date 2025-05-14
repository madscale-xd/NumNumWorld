using UnityEngine;

public class AuraPanelTrigger : MonoBehaviour
{
    public GameObject panel; // Assign your panel here in the Inspector
    public CanvasGroup canvasGroup;
    
    void Start(){
        canvasGroup = panel.GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aura"))
        {
            TogglePanel();
        }
    }

    // Public method so other scripts can toggle it too
    public void TogglePanel()
    {
        if (panel != null)
            panel.SetActive(!panel.activeSelf);
    }

    // Optional method to force-show or hide
    public void ShowPanel()
    {
        if (panel != null)
            panel.SetActive(true);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;         // Make invisible
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void HidePanel()
    {
        if (panel != null)
            panel.SetActive(false);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}
