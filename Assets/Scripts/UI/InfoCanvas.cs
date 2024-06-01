using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.UI
{
  public class InfoCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _pointsText;

    [SerializeField]
    private Button _exitAIPlayModeButton;

    public void SetPoints(int points)
    {
      _pointsText.text = points.ToString();
    }

    public void ShowExitAIPlayModeButton()
    {
      _exitAIPlayModeButton.gameObject.SetActive(true);
    }

    public void HideExitAIPlayModeButton()
    {
      _exitAIPlayModeButton.gameObject.SetActive(false);
    }
  }
}
