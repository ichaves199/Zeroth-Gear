using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMovement : MonoBehaviour {

    public string sceneToLoad;
    private CameraMovement cam;
    public AudioSource source;
    public CameraFade fade;
    public PlayerMovement player;


    // Start is called before the first frame update
    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // TODO: add music and camera fade in/out
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !other.isTrigger) {
            StartCoroutine(levelTransition(other));
        }
    }

    private IEnumerator levelTransition(Collider2D other) {

        // lock player movement
        player.lockMovement = true;

        // fade camera
        fade.Fade(0.75f);

        // fade audio
        float startVolume = 1;
        float fadeTime = 0.75f;
        while (source.volume > 0) {
            source.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        source.Stop();

        yield return new WaitForSeconds(0.75f);
        
        // load new scene
        SceneManager.LoadScene(sceneToLoad);

    }

}

