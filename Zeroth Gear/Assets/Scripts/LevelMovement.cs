using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class LevelMovement : MonoBehaviour {

    public string sceneToLoad;
    public Vector2 cameraNewMin;
    public Vector2 cameraNewMax;
    public Vector3 playerNewPos;
    private CameraMovement cam;
    public bool announceArea;
    public string areaName;
    public GameObject text;
    public Text areaText;
    public bool changeTrack;
    public AudioSource source;
    public AudioClip clip;
    public CameraFade fade;


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
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // private IEnumerator levelTransition(Collider2D other) {
    //     if(changeTrack) {
    //         StartCoroutine(MusicTransition(source, 1f, clip));
    //     }

    //     fade.Fade(2f);
    //     yield return new WaitForSeconds(0.5f);

    //     SceneManagement.LoadScene(sceneToLoad);

    //     if(announceArea) {
    //         StartCoroutine(placeNameCo());
    //     }

        
    // }

    private IEnumerator placeNameCo() {
        text.SetActive(true);
        areaText.text = areaName;
        yield return new WaitForSeconds(2.5f);
        text.SetActive(false);
    }

    private IEnumerator MusicTransition(AudioSource audioSource, float FadeTime, AudioClip newClip) {
        float startVolume = 1;
 
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Stop();
        source.clip = clip;
        source.Play();
        while (audioSource.volume < startVolume) {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
        audioSource.volume = startVolume;
        
    }

}

