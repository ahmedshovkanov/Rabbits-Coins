using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageResizeHandler : MonoBehaviour
{
    [SerializeField] private bool _resizeByAnchors = true, _resizeByImageReplace = false, _resizeByPosition;

    [SerializeField] private Sprite _portImage, _landImage;
    [SerializeField] private Vector2 _portPos = new Vector2(0f, 212f), _landPos = new Vector2(0f, 95f);
    [SerializeField] private Vector2 _portSize = new Vector2(237f, 237f), _landSize;

    [SerializeField] private Vector2 _anchorsPortMin, _anchorsPortMax;

    [SerializeField] private Vector2 _anchorsLandMin, _anchrsLandMax;
    [SerializeField] private ScreenOrientation _lastScreenOrientation;

    private RectTransform _rect;
    private Image _image;
    private bool _OnEnableCostul = false;

    private void Awake()
    {
        _rect = this.GetComponent<RectTransform>();
        _image = this.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (_lastScreenOrientation != Screen.orientation)
        {
            ChangeOrientation();
            _lastScreenOrientation = Screen.orientation;
        }
    }

    private void ChangeOrientation()
    {
        Resize();

    }

    private void Start()
    {
        Resize();

        _OnEnableCostul = true;
    }


    [ContextMenu("Resize")]
    private void Resize()
    {
        if (_resizeByAnchors)
        {
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                _rect.anchorMin = _anchorsPortMin;
                _rect.anchorMax = _anchorsPortMax;
            }
            else
            {
                _rect.anchorMin = _anchorsLandMin;
                _rect.anchorMax = _anchrsLandMax;
            }
        }
        if (_resizeByImageReplace)
        {
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                _image.sprite = _portImage;
            }
            else
            {
                _image.sprite = _landImage;
            }
        }
        if (_resizeByPosition)
        {
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            {
                _rect.anchoredPosition = _portPos;
                _rect.sizeDelta = _portSize;
            }
            else
            {
                _rect.anchoredPosition = _landPos;
                _rect.sizeDelta = _landSize;
            }
        }
    }
}
