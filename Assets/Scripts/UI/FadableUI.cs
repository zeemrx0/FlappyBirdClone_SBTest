using System.Collections;
using UnityEngine;

namespace LNE.UI
{
  [RequireComponent(typeof(CanvasGroup))]
  public class FadableUI : MonoBehaviour
  {
    [SerializeField]
    private float _fadeInDuration = 0.1f;

    [SerializeField]
    private float _fadeOutDuration = 0.1f;

    private CanvasGroup _canvasGroup;

    public bool IsVisible => _canvasGroup.alpha > 0;

    private void Awake()
    {
      _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
      StartCoroutine(FadeIn());
      
    }

    private IEnumerator FadeIn()
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
      _canvasGroup.blocksRaycasts = true;
      _canvasGroup.interactable = true;
    }

    public void Hide()
    {
      StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
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
      _canvasGroup.blocksRaycasts = false;
      _canvasGroup.interactable = false;
    }
  }
}
