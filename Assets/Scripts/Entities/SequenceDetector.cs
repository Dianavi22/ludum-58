using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDetector : MonoBehaviour
{
    private static readonly KeyCode[] _konamiCode = new KeyCode[] {
        KeyCode.UpArrow, KeyCode.UpArrow,
        KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow,
        KeyCode.B, KeyCode.A, // Treated as Q on AZERTY!
        KeyCode.Return
    };

    /// <summary>
    /// Time two key input before the sequence is reseted.
    /// </summary>
    [SerializeField] private float _sequenceTimeOut = 2f;

    [SerializeField] private SuccessMapManager _successManager;

    private int _currentIndex;
    private float _lastInputTime;

    private void Update()
    {
        if (_sequenceTimeOut < Time.time - _lastInputTime && _currentIndex != 0)
        {
            ResetSequence();
        }


        if (Input.GetKeyDown(_konamiCode[_currentIndex]))
        {
            _currentIndex++;
            _lastInputTime = Time.time;

            if (_konamiCode.Length <= _currentIndex)
            {
                SequenceCompleted();
                ResetSequence();
            }
        }
        else if (Input.anyKeyDown && _currentIndex != 0)
        {
            ResetSequence();
        }
    }

    private void SequenceCompleted()
    {
        _successManager.LaunchSuccessAnim(PlayerPrefsData.KONAMI_CODE);
    }

    private void ResetSequence()
    {
        _currentIndex = 0;
        _lastInputTime = 0;
    }
}
