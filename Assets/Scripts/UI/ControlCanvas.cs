using UnityEngine;

namespace LNE.UI
{
  public class ControlCanvas : MonoBehaviour
  {
    [SerializeField]
    private ToggleButton _toggleAIPlayModeButton;

    public void SetActive(bool isActive)
    {
      gameObject.SetActive(isActive);
    }

    public void SetToggleAIPlayModeButtonActive(bool isActive)
    {
      _toggleAIPlayModeButton.gameObject.SetActive(isActive);
    }

    public void SetToggleAIPlayModeButtonState(bool isOn)
    {
      _toggleAIPlayModeButton.SetState(isOn);
    }
  }
}
