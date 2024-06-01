using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class InfoCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _pointsText;

    public void SetPoints(int points)
    {
      _pointsText.text = points.ToString();
    }
  }
}
