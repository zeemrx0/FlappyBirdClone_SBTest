using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class InputUsernameCanvas : MonoBehaviour
  {
    [SerializeField]
    private TMP_InputField _usernameInputField;

    [SerializeField]
    private TMP_Text _rankText;

    public void SetActive(bool isActive)
    {
      gameObject.SetActive(isActive);
    }

    public string GetUsername()
    {
      return _usernameInputField.text;
    }

    public void SetRankText(string rankText)
    {
      _rankText.text = rankText;
    }
  }
}
