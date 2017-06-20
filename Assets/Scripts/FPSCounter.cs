using UnityEngine;

public class FPSCounter : MonoBehaviour {

    public int frameRange = 60;
    public int AverageFPS { get; private set; }

    void Update ()
    {
        AverageFPS = (int)(1f / Time.unscaledDeltaTime);
    }
}
