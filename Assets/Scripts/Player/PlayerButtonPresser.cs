using System.Linq;
using UnityEngine;

public class PlayerButtonPresser : MonoBehaviour
{    
    private Game _game;
    private Player _player;

    public void Init(Game game, Player player) {
        _game = game;
        _player = player;
    }

    public void TryPress() {
        RaycastHit[] hits = Physics.RaycastAll(_player.cameraHolder.position, _player.cameraHolder.forward);
        
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.CompareTag("Player")) continue;
            if (hit.collider.TryGetComponent(out PressableObject pressable)) {
                Press(pressable);
                return;
            }  else 
                return;
        }
    }

    private void Press(PressableObject pressable) {
        pressable.OnPress();
    }
}
