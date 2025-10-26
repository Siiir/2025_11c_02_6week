using interactibles;

namespace death_effects
{
    public class RespawnsAtCheckPoint : Respawning
    {
        public override void Terminate()
        {
            // Avoid re-checking in at the same checkpoint upon respawn
            var checkPoint = respawnTransform.GetComponent<CheckPoint>();
            // Since respawn might not be a checkpoint
            if (checkPoint != null)
            {
                checkPoint
                    .SuppressNextCheckInFor(gameObject);
            }

            base.Terminate();
        }
    }
}