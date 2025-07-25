using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM")]
    public AudioClip mainMenuBGM;
    public AudioClip gameBGM;
    public AudioClip gameOverBGM;

    [Header("SFX")]
    public AudioClip sfxButton;
    public AudioClip sfxMinusHealth;
    public AudioClip sfxPlayerAttack;
    public AudioClip sfxEnemyMinusHealth;
    public AudioClip sfxEnemyDeath;
    public AudioClip sfxPlayerDeath;
    public AudioClip sfxParry;
    public AudioClip sfxPlayerHeal;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            sfxSource.ignoreListenerPause = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
            PlayBGM(mainMenuBGM);
        else if (scene.name == "NumNumMain")
            PlayBGM(gameBGM);
        else if (scene.name == "EndScene")
            PlayBGM(gameOverBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Separated SFX methods
    public void PlayButtonSFX()
    {
        PlaySFX(sfxButton);
    }

    public void PlayMinusHealthSFX()
    {
        PlaySFX(sfxMinusHealth);
    }

    public void PlayPlayerAttackSFX()
    {
        PlaySFX(sfxPlayerAttack);
    }

    public void PlayEnemyMinusHealthSFX()
    {
        PlaySFX(sfxEnemyMinusHealth);
    }

    public void PlayEnemyDeathSFX()
    {
        PlaySFX(sfxEnemyDeath);
    }

    public void PlayPlayerDeathSFX()
    {
        PlaySFX(sfxPlayerDeath);
    } 
    
    private IEnumerator HandlePlayerDeathAndSceneLoad()
    {
        // Stop BGM
        AudioManager.Instance.bgmSource.Stop();

        // Play death SFX
        AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxPlayerDeath);

        // Wait for SFX duration
        yield return new WaitForSeconds(AudioManager.Instance.sfxPlayerDeath.length);

        // Now load EndScene
        SceneManager.LoadScene("EndScene");
    }


    public void PlayParrySFX()
    {
        PlaySFX(sfxParry);
    }

    public void PlayPlayerHealSFX()
    {
        PlaySFX(sfxPlayerHeal);
    }
}
