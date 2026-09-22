using UnityEngine;

public class Player : MonoBehaviour, IHealthy
{
    private Game _game;
    public Health health;
    public InputRouter input;
    public PlayerMovement movement;
    public PlayerSound sound;

    public void Init(Game game) {
        _game = game;

        health.Init(game, this);
        movement.Init(game, this);
        sound.Init(game, this);  
        input.Init(game, this);
    }
  
    private void Update() {
        input.ManualUpdate(Time.deltaTime);        
        movement.ManualUpdate();
    }

    public void FixedUpdate() {
        input.ManualFixedUpdate();
        movement.ManualFixedUpdate();
    }
    
    public void Death() {       
        sound.OnDeath();
    }

    public void Heal() { }
    public void NonLetalDamage() { }
    public Health GetHealth() => health;
}