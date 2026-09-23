using UnityEngine;

public class PressableObject : MonoBehaviour
{
    public SceneBootChannel sceneBootChannel;
    public void OnPress() {
        sceneBootChannel.boot.activeGame.session.OnNextButtonPress();
    }
}
