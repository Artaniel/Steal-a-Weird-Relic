using UnityEngine;

public class PlayerButtonPresser : MonoBehaviour
{    
    private Game _game;
    private Player _player;

    public void Init(Game game, Player player) {
        _game = game;
        _player = player;
    }

    public void OnPress() {
        RaycastHit[] hits = Physics.RaycastAll(_player.cameraHolder.position, _player.cameraHolder.forward);
    }
}
