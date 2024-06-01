using UnityEngine;
using UnityEngine.UI;

namespace LNE.UI
{
  public class ToggleButton : MonoBehaviour
  {
    [SerializeField]
    private Sprite _onSprite;

    [SerializeField]
    private Sprite _offSprite;

    private Image _image;

    private void Awake()
    {
      _image = GetComponent<Image>();
    }

    public void SetState(bool isOn)
    {
      _image.sprite = isOn ? _onSprite : _offSprite;
    }
  }
}
