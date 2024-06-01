namespace LNE.Birds
{
  public class BirdMovementModel
  {
    public float VerticalSpeed { get; set; }
    public float TimeUntilNextFlap { get; set; }

    public BirdMovementModel()
    {
      VerticalSpeed = 0f;
      TimeUntilNextFlap = 0f;
    }
  }
}
