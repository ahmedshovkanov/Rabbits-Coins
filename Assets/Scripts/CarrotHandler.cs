using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarrotHandler : MonoBehaviour
{
    private CarrotData _carrot;
    public TMP_Text TimeLeftText;
    float growthTimeLeft;
    private Image _imageRef;
    private bool _isFinished;
    public void InitHandler(CarrotData data)
    {
        _carrot = data;
        _imageRef = GetComponent<Image>();

        TimeSpan timeSincePlanted = DateTime.Now - _carrot.plantDate;
        //Debug.Log(timeSincePlanted)
        float secondsSincePlanted = (float)timeSincePlanted.TotalSeconds;

        // Calculate time left for carrot to grow
        growthTimeLeft = _carrot.growthTimeSeconds - secondsSincePlanted;

        // Ensure time left doesn't go below zero (carrot is already fully grown)
       // growthTimeLeft = Mathf.Max(0f, growthTimeLeft);

        Debug.Log($"Time left for carrot to grow: {growthTimeLeft} seconds");

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
        }
    }

    private IEnumerator TimerHandler()
    {
        while(!_isFinished)
        {
            
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
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public Sprite[] CarrotsSpritesFromNewToGrowed;
}
