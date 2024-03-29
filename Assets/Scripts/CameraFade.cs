using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public float speedScale = 0.5f;
    public Color fadeColor = Color.black;
    // Rather than Lerp or Slerp, we allow adaptability with a configurable curve
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;


    private float alpha = 0f; 
    private Texture2D texture;
    private int direction = 0;
    private float time = 0f;
    private int done = 0;

    private void Start()
    {
        if (startFadedOut) alpha = 1f; else alpha = 0f;
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();

    }

    public void Fade(float nspeedScale = 0.5f) {
        done = 0;
        time = 0f;
        speedScale = nspeedScale;
    }

    private void Update()
    {
        if (direction == 0 && done == 0)
        {
            if (alpha >= 1f) // Fully faded out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;
                done = 1;
                Debug.Log(alpha);
            }
            else // Fully faded in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;
                done = 1;
                Debug.Log(alpha);
            }
                 
        }
    }
    public void OnGUI()
    {
        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
            if (alpha <= 0f || alpha >= 1f) direction = 0;
        }
    }
}