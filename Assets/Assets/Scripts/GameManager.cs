using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public int lives = 5;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int score = 0;
    public ParticleSystem explosion;



    //Functions
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 0.75)
        {
            score += 100;
        }
        else if (asteroid.size > 1)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }


    public void PlayerDied()
    {
        this.lives--;
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
