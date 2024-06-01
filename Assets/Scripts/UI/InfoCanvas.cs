using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class InfoCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _pointsText;

    [SerializeField]
    private ToggleButton _exitAIPlayModeButton;

    public void SetPoints(int points)
    {
      _pointsText.text = points.ToString();
    }

    public void SetExitAIPlayModeButtonState(bool isOn)
    {
      _exitAIPlayModeButton.SetState(isOn);
    }
  }
}
