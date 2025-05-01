using UnityEngine;

public class PlayMenuView : View
{
    public override void Initialize() { }

    public void OnStartClicked()
    {
        ViewManager.Instance.LoadGame(); // or call Start Game logic
    }
}
