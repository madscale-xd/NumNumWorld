using UnityEngine;

public class PanelTrigger : MonoBehaviour
{
    public GameObject panel; // Assign your panel here in the Inspector

    // Public method to toggle the panel without any trigger
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
