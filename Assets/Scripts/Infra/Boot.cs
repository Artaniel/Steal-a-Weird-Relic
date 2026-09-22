using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    public SceneBootChannel bootChannel;
    public Game activeGame;    

    private void Awake() {
        if (bootChannel.boot) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        bootChannel.boot = this;

        SceneManager.LoadScene("Museum"); //TODO: async load after SDK connected
    }

    public void OnBootCreated(Game game) {
        activeGame = game;
        game.Init(this);
    }
} 