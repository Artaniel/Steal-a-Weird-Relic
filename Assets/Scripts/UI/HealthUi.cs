using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    private Game _boot;
    private UI _ui;

    public Image[] heartImages;

    public void Init(Game boot, UI ui) {
        _boot = boot;
        _ui = ui;
    }

    public void Refresh(int value) {
        value = Mathf.Clamp(value, 0, 3);
        heartImages[0].enabled = value >= 1;
        heartImages[1].enabled = value >= 2;
        heartImages[2].enabled = value >= 3;
    }
}
