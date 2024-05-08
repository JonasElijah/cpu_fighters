using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 5;
    public TextMeshProUGUI countdownDisplay; // Change Text to TextMeshProUGUI

    private void Start()
    {
        if (countdownDisplay == null)
        {
            Debug.LogError("Missing countdownDisplay reference on " + gameObject.name);
            return;
        }

        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        // StartGame(); // Call any additional methods here
    }
}
