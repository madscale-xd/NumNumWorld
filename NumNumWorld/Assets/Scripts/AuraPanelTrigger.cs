using UnityEngine;

public class AuraPanelTrigger : MonoBehaviour
{
    public GameObject panel; // Assign your panel here in the Inspector

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
    public void ShowPanel(bool show)
    {
        if (panel != null)
            panel.SetActive(show);
    }
}
