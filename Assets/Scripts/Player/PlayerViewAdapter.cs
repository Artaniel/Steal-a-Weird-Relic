using System.Collections.Generic;
using UnityEngine;

public class PlayerViewAdapter : MonoBehaviour
{
    public Animator animator;
    public GameObject assault;
    public GameObject sniper;
    public GameObject lmg;

    public List<SkinnedMeshRenderer> skinRenderers;
    public List<SkinnedMeshRenderer> hairRenderers;

    public Material skinBlue;
    public Material skinRed;
    public Material hairBlue;
    public Material hairRed;

    public Renderer[] rifleRenderers;
    public Renderer[] lmgRenderers;
    public Renderer[] sniperRenderers;

    public GameObject highlightObject;

    public void ColorChangeBlue() {
        foreach (SkinnedMeshRenderer renderer in skinRenderers) {
            renderer.material = skinBlue;
        }
        foreach (SkinnedMeshRenderer renderer in hairRenderers) {
            renderer.material = hairBlue;
        }
    }   
    
    public void ColorChangeRed() {
        foreach (SkinnedMeshRenderer renderer in skinRenderers) {
            renderer.material = skinRed;
        }
        foreach (SkinnedMeshRenderer renderer in hairRenderers) {
            renderer.material = hairRed;
        }
    }

    public void HighLight(float time) {
        highlightObject.SetActive(true);
    }

    public void DisableHighlight() {
        highlightObject.SetActive(false);
    }
}
