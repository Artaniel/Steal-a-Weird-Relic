using UnityEngine;

public class Saveloading : MonoBehaviour
{
    private Game _boot;

    public void Init(Game boot) {
        _boot = boot;
        Load();
    }

    public void Save() {
    }

    public void Load() {
    }
}
