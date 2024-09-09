using UnityEngine;

public class PauseParticles : MonoBehaviour
{
    bool isOnPlay;
    [SerializeField] ParticleSystem[] particles;
    void Start()
    {
        GameManager.GetInstance().OnGameStateChanged += OnGameStateChanged;
        OnGameStateChanged(GameManager.GetInstance().currentGameState);
    }

    void OnGameStateChanged(GAME_STATE _gs)
    {
        isOnPlay = _gs == GAME_STATE.PLAY;
        for (int i = 0; i < particles.Length; i++)
        {
            if (isOnPlay)
            {
                particles[i].Play();
            }
            else
            {
                particles[i].Pause();
            }
        }
    }
}