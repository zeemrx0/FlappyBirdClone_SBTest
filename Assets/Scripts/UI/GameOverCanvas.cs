using TMPro;
using UnityEngine;

namespace LNE.UI
{
  public class GameOverCanvas : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _pointsText;

    public void SetPoints(int points)
    {
      _pointsText.text = points.ToString();
    }
  }
}
