using System.Collections;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public delegate void OnTimerFinished();

    public void StartCountDownTimer(float start, OnTimerFinished onTimerFinished)
    {
        StartCoroutine(CountDownRoutine(start, onTimerFinished));
    }

    private IEnumerator CountDownRoutine(float start, OnTimerFinished onTimerFinished)
    {
        float current = start;

        while(current > 0f)
        {
            current -= 1f * Time.deltaTime;

            Debug.Log($"Timer: {current:F2}");

            //! if(current < 0) // This a commun mistake
            //    yield return null;

            // This is what tell the program to yield controll for one frame before continuing
            // subtracting the current time inside the while loop, so the app does not crashes.
            // By using a condition before, the while loop will continue running
            // durring the current frame, crashing the app
            yield return null;
        }

        onTimerFinished?.Invoke();
    }
}
