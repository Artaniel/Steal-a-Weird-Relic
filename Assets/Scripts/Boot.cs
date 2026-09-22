using UnityEngine;

public class Boot : MonoBehaviour
{
    public SceneBootChannel bootChannel;
    public Game activeBoot;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        bootChannel.boot = this;
    }

    public void OnBootCreated(Game game) {
        activeBoot = game;
        game.Init(this);
    }
} 