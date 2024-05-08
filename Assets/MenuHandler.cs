using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuHandler : MonoBehaviour
{
    public GameObject fight;
    public GameObject settings;
    public GameObject quit;

    public void Start()
    {
        PlaySound(fight); 
    }
    
    public void Play()
    {
        StartCoroutine(PlaySoundAndLoadScene(fight, "CharacterSelect"));
    }

    public void Settings()
    {
        StartCoroutine(PlaySoundAndLoadScene(settings, "Settings"));
    }

    public void Quit()
    {
        StartCoroutine(PlaySoundAndQuit(quit));
    }

    IEnumerator PlaySoundAndLoadScene(GameObject obj, string sceneName)
    {
        AudioSource audio = obj.GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length); 
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator PlaySoundAndQuit(GameObject obj)
    {
        AudioSource audio = obj.GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length); 
        Application.Quit();
    }

    public void PlaySound(GameObject obj)
    {
        obj.GetComponent<AudioSource>().Play();
    }
}
