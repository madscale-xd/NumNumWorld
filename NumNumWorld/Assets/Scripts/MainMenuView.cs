using UnityEngine;

public class MainMenuView : View
{
    public override void Initialize() { }

    public void OnPlayClicked()
    {
        ViewManager.Show<PlayMenuView>();
    }

    public void OnQuitClicked()
    {
        ViewManager.Instance.QuitGame(); // or ViewManager.QuitGame() if static
    }
}
