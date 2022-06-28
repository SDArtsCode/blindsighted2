using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        PlayerHealth.onPlayerDeath += PlayerDeath;

        anim = GetComponent<Animator>();
    }

    public void PlayerDeath()
    {
        anim.SetTrigger("Start");
    }

    public void AnimComplete()
    {
        LevelLoader.instance.LoadLevel(0, -1);
    }

    private void OnDestroy()
    {
        PlayerHealth.onPlayerDeath -= PlayerDeath;
    }
}
