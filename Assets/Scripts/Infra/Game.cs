using UnityEngine;

public class Game : MonoBehaviour
{    
    private Boot _boot;
    public SceneBootChannel bootChannel;

    public UI ui;
    public Session session;
    public Camera mainCamera;
    public Sound sound;
    public Monetization monetization;
    public Saveloading saveloading;
    public Map map;

    public Player player;

    public Library library;


    private void Awake() {
        bootChannel.GameCreatedSignal(this);
        mainCamera = Camera.main;
        ui.Init(this);
        sound.Init(this);
        monetization?.Init(this);
        session.Init(this);
        player.Init(this);
    }

    public void Init(Boot boot) {
        _boot = boot;
    }

    private void Start() {
        saveloading?.Init(this);
    }
}
