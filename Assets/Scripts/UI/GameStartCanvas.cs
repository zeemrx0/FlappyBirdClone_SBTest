using UnityEngine;

namespace LNE.UI
{
  public class GameStartCanvas : MonoBehaviour
  {
    private FadableUI _fadableUI;

    private void Awake()
    {
      _fadableUI = GetComponent<FadableUI>();
    }

    public void SetActive(bool isActive)
    {
      if (isActive)
      {
        _fadableUI.Show();
      }
      else
      {
        _fadableUI.Hide();
      }
    }
  }
}
