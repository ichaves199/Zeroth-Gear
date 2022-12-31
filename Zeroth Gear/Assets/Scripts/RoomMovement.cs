using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMovement : MonoBehaviour {

    public Vector2 cameraChangeMin;
    public Vector2 cameraChangeMax;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool announceArea;
    public string areaName;
    public GameObject text;
    public Text areaText;
    public bool changeTrack;
    public AudioSource source;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            cam.minPosition = cameraChangeMin;
            cam.maxPosition = cameraChangeMax;

            other.transform.position += playerChange;

            if(announceArea) {
                StartCoroutine(placeNameCo());
            }

            if(changeTrack) {
                StartCoroutine(MusicTransition(source, 1f, clip));
                
            }
        }
    }

    private IEnumerator placeNameCo() {
        text.SetActive(true);
        areaText.text = areaName;
        yield return new WaitForSeconds(2.5f);
        text.SetActive(false);
    }

    private IEnumerator MusicTransition(AudioSource audioSource, float FadeTime, AudioClip newClip) {
        float startVolume = audioSource.volume;
 
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
