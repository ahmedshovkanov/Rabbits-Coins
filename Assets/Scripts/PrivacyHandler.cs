using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyHandler : MonoBehaviour
{
    public string Link;
    private UniWebView _screen;

    public void LoadURL()
    {
        Debug.Log("LoadURL()");
        if (_screen == null)
        {
            GameObject webViewGameObject = new GameObject("PrivacyPolicyScreen");
            _screen = webViewGameObject.AddComponent<UniWebView>();
            _screen.Frame = Screen.safeArea;
            _screen.OnPageFinished += OnPageFinished;
            _screen.OnShouldClose += OnShouldClose;
            _screen.EmbeddedToolbar.Show();
            _screen.SetAllowBackForwardNavigationGestures(true);
        }

        _screen.Load(Link);
        _screen.Show();
    }

    GameObject _bg;

    private void OnPageFinished(UniWebView webView, int statusCode, string url)
    {
        GameObject bg = new GameObject("BG");
        bg.AddComponent<Image>();
        bg.GetComponent<Image>().color = Color.black;
        bg.GetComponent<RectTransform>().sizeDelta = new Vector2(2000, 5000);
        bg.transform.SetParent(GameObject.Find("UI").transform, false);
        _bg = bg;
        StartCoroutine(CloseScreen());
    }

    private bool OnShouldClose(UniWebView webView)
    {
        if (webView != null)
            Destroy(webView.gameObject);
        webView = null;
        if (_bg != null)
            Destroy(_bg);
        StopAllCoroutines();
        return true;
    }

    public void Close()
    {
        if (_screen != null)
        {
            _screen.Hide();
            Destroy(_screen.gameObject);
            _screen = null;
        }
        if (_bg != null)
            Destroy(_bg);
        StopAllCoroutines();
    }

    IEnumerator CloseScreen()
    {
        Vector3 firstTouchPos, secondTouchPos;
        while (true)
        {
            yield return new WaitWhile(() => !Input.GetMouseButtonDown(0));
            firstTouchPos = Input.mousePosition;
            Debug.Log(firstTouchPos);
            yield return new WaitWhile(() => !Input.GetMouseButtonUp(0));
            secondTouchPos = Input.mousePosition;
            Debug.Log(secondTouchPos);
            if ((secondTouchPos - firstTouchPos).x > 200)
            {
                Close();
            }
        }
    }
}