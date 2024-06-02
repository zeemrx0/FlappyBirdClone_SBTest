using System.Collections;
using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class InfoCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private FadableUI _AIModeMessage;

    public void SetActive(bool isActive)
    {
      gameObject.SetActive(isActive);
    }

    public void SetScore(int score)
    {
      _scoreText.text = score.ToString();
    }

    public void ShowAIModeMessage(float delay)
    {
      if (_AIModeMessage.IsVisible)
      {
        return;
      }

      _AIModeMessage.Show();
      StartCoroutine(HideAIModeMessageCoroutine(delay));
    }

    public IEnumerator HideAIModeMessageCoroutine(float delay)
    {
      yield return new WaitForSeconds(delay);
      _AIModeMessage.Hide();
    }
  }
}
