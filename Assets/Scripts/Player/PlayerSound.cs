using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player _player;
    private Game _game;

    public AudioSource shoot;
    public AudioSource landing;
    public AudioSource kill;
    public AudioSource footstep;
    public AudioSource jump;

    public void Init(Game game, Player player) {
        _game = game;
        _player = player;
    }

    public void OnShoot() { shoot?.Play(); }
    public void OnLand() { landing?.Play(); }
    public void OnDeath() { kill?.Play(); }
    public void OnFootstep() { footstep?.Play(); }
    public void OnJump() { jump?.Play(); }
}
