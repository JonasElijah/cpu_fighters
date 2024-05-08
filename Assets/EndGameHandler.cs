using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour
{
    public GameObject play;
    public GameObject quit;

    public void PlayAgain()
    {
        StartCoroutine(PlaySoundAndLoadScene(play, "MainMenu"));
    }

    public void Quit()
    {
        PlaySoundAndQuit(quit);
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
}
