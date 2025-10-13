using death_effects.interfaces;

namespace death_processors
{
    /// It's a mortal that can process additional death effects.
    public class AgonyfulMortal : Mortal
    {
        public sealed override void Die()
        {
            foreach (var effect in this.GetComponents<IPreDeath>())
            {
                effect.DoPreDeath();
            }
            base.Die();
            foreach (var effect in this.GetComponents<IPostDeath>())
            {
                effect.DoPostDeath();
            }
        }
    }
}