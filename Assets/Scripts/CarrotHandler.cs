using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarrotHandler : MonoBehaviour
{
    private CarrotData _carrot;
    public TMP_Text timeText;
    float growthTimeLeft;
    private Image _imageRef;
    private bool _isFinished;

    [SerializeField] private float seconds;
    public void InitHandler(CarrotData data)
    {
        _carrot = data;
        _imageRef = GetComponent<Image>();
        _isFinished = false;

        TimeSpan timeSincePlanted = DateTime.Now - _carrot.plantDate;
        //Debug.Log(timeSincePlanted)
        float secondsSincePlanted = (float)timeSincePlanted.TotalSeconds;

        // Calculate time left for carrot to grow
        growthTimeLeft = _carrot.growthTimeSeconds - secondsSincePlanted;
        Debug.Log("growthTimeLeft " + growthTimeLeft);
        // Ensure time left doesn't go below zero (carrot is already fully grown)
        // growthTimeLeft = Mathf.Max(0f, growthTimeLeft);

        //Debug.Log($"Time left for carrot to grow: {growthTimeLeft} seconds");

        // Optional: Convert to more readable format
        //TimeSpan timeLeft = TimeSpan.FromSeconds(growthTimeLeft);
        //Debug.Log($"Time left: {timeLeft.Hours}h {timeLeft.Minutes}m {timeLeft.Seconds}s");

        StartCoroutine(TimerHandler());
    }

    public void HandlerClick()
    {
        if(_isFinished)
        {
            SaveSystem.Instance.CurActivePerson.Coins += 1 + this._carrot.interestRateWhenPlanted;
            SaveSystem.Instance.CurActivePerson.carrots.Remove(_carrot);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator TimerHandler()
    {
        while(!_isFinished)
        {
            Debug.Log("growthTimeLeft " + growthTimeLeft);
            growthTimeLeft--;

            if(growthTimeLeft <= 0)
            {
                
                _imageRef.sprite = CarrotsSpritesFromNewToGrowed[2];
                _isFinished = true;
            } else
            {
                if (growthTimeLeft > 3600)
                {
                    if(growthTimeLeft > 28000)
                    {
                        _imageRef.sprite = CarrotsSpritesFromNewToGrowed[0];
                    } else
                    {
                        _imageRef.sprite = CarrotsSpritesFromNewToGrowed[1];
                    }
                }
            }
            UpdateTimeDisplay();
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetSeconds(float newSeconds)
    {
        seconds = newSeconds;
        UpdateTimeDisplay();
    }

    private void UpdateTimeDisplay()
    {
        if (timeText == null) return;

        timeText.text = FormatTime(growthTimeLeft);
    }

    private string FormatTime(float totalSeconds)
    {
        if (totalSeconds < 0f)
        {
            return "0";
        }
        else if (totalSeconds >= 3600f) // 1 hour or more
        {
            float hours = totalSeconds / 3600f;
            return $"{hours:F1} h"; // Format with 1 decimal place
        }
        else if (totalSeconds >= 60f) // 1 minute or more
        {
            int minutes = (int)(totalSeconds / 60f);
            int seconds = (int)(totalSeconds % 60f);
            return $"{minutes}:{seconds:D2}"; // Format as minutes:seconds
        }
        else // Less than 1 minute
        {
            int seconds = (int)totalSeconds;
            return $"0:{seconds:D2}"; // Format as 0:seconds
        }
    }

    // Optional: Method to handle integer seconds
    public void SetSeconds(int secondsInt)
    {
        SetSeconds((float)secondsInt);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable/destroy Carrot");
        StopAllCoroutines();
    }

    public Sprite[] CarrotsSpritesFromNewToGrowed;
}
