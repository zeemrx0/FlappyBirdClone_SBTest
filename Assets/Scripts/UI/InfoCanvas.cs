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
    private ToggleButton _exitAIPlayModeButton;

    [SerializeField]
    private FadableUI _AIModeMessage;

    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
      _scoreText.text = score.ToString();
    }

    public void SetExitAIPlayModeButtonState(bool isOn)
    {
      _exitAIPlayModeButton.SetState(isOn);
    }

    public void ShowAIModeMessage(float delay)
    {
      if (_AIModeMessage.IsVisible)
      {
        return;
      }

      _AIModeMessage.Show();
      StartCoroutine(HideAIModeMessage(delay));
    }

    public IEnumerator HideAIModeMessage(float delay)
    {
      yield return new WaitForSeconds(delay);
      _AIModeMessage.Hide();
    }
  }
}
