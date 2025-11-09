using death_effects;

public class TotemTerminatesWithAnimation : TerminatesInsteadOfDying
{
    public override void DoDeath()
    {
        var totemDeathAnimationHandler = GetComponent<TotemDeathAnimationHandler>();
        if (totemDeathAnimationHandler)
        {
            totemDeathAnimationHandler.PlayDeathAnimation();
        }
        else
        {
            base.DoDeath();
        }
    }
}