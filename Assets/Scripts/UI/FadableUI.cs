using System.Collections;
using UnityEngine;

namespace LNE.UI
{
  [RequireComponent(typeof(CanvasGroup))]
  public class FadableUI : MonoBehaviour
  {
    [SerializeField]
    private float _fadeInDuration = 0.5f;

    [SerializeField]
    private float _fadeOutDuration = 0.5f;

    private CanvasGroup _canvasGroup;

    public bool IsVisible => _canvasGroup.alpha > 0;

    private void Awake()
    {
      _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
      _canvasGroup.alpha = 1;
      StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
      _canvasGroup.alpha = 0;

      var time = 0f;

      while (time < _fadeInDuration)
      {
        _canvasGroup.alpha = time / _fadeInDuration;
        time += Time.deltaTime;
        yield return null;
      }

      _canvasGroup.alpha = 1;
    }

    public void Hide()
    {
      _canvasGroup.alpha = 0;
      StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
      _canvasGroup.alpha = 1;

      var time = 0f;

      while (time < _fadeOutDuration)
      {
        _canvasGroup.alpha = 1 - time / _fadeOutDuration;
        time += Time.deltaTime;
        yield return null;
      }

      _canvasGroup.alpha = 0;
    }
  }
}
