using interactibles;

namespace death_effects
{
    public class RespawnsAtCheckPoint : Respawning
    {
        public override void Terminate()
        {
            // Avoid re-checking in at the same checkpoint upon respawn
            respawnTransform.GetComponent<CheckPoint>()
                .SuppressNextCheckInFor(gameObject);
            base.Terminate();
        }
    }
}