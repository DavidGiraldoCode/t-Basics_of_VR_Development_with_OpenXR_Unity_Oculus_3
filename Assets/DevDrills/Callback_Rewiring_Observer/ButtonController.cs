using System;
using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private enum EVENTS_OPTIONS
    {
        Delegate,
        Action
    }
    [SerializeField]
    private EVENTS_OPTIONS _currrentEventOption;
    [SerializeField]
    private CountdownTimer countdownTimer;
    public TMP_Text text;

    //* Classic Option to create custom delegates -------------
    public delegate void OnButtonPressed();
    public event OnButtonPressed buttonPressed;

    // --------------------------------------------------------

    //* Classic Option to create custom delegates -------------
    public Action OnButtonActionPressed;
    // --------------------------------------------------------
    public void RaiseEvent()
    {
        switch(_currrentEventOption)
        {
            case EVENTS_OPTIONS.Delegate:
                if(buttonPressed.GetInvocationList().Length > 0)
                    buttonPressed.Invoke();

                // buttonPressed?.Invoke(); // Can also use this, without the if
            break;
            case EVENTS_OPTIONS.Action:
                OnButtonActionPressed.Invoke();
                countdownTimer.StartCountDownTimer(5, Notify);
            break;
        }
    }

    /// <summary>
    /// Method that is passed as callback
    /// </summary>
    private void Notify()
    {
        Debug.Log("Timer eneded!");
    }
    //
    private void Awake()
    {
        countdownTimer = GetComponent<CountdownTimer>();   
    }

    void Update()
    {

    }
}
