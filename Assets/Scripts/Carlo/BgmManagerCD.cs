using E7.Introloop;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManagerCD : SingletonCD<BgmManagerCD>
{
    [Header("Audio Source Settings")]
    public AudioSource TemplateSourcePrefab;
    public AudioSource TemplateSource;

    [Header("Background Music Settings")]
    public IntroloopAudio HuntBgm;
    public IntroloopAudio NormalBgm;

    BgmPlayingState currentBgmState;

    enum BgmPlayingState
    {
        None,
        Hunt,
        Normal,
        Stopped
    }


    public override void Awake()
    {
        base.Awake();

        TemplateSource = Instantiate(TemplateSourcePrefab);

        // Must call before the first access.
        //if (TemplateSource != null) IntroloopPlayer.SetSingletonInstanceTemplateSource(TemplateSource);

        // Spawned singleton instance under DontDestroyOnLoad now spawned with
        // applied settings from the template source!
        //IntroloopPlayer.Instance.Play(audioToPlay);
        PlayNormalBgm();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        StopBgm();
    }

    public void PlayHuntBgm()
    {
        if (currentBgmState != BgmPlayingState.Hunt)
        {
            currentBgmState = BgmPlayingState.Hunt;
            IntroloopPlayer.Instance.Play(HuntBgm);
        }
        else return;
    }

    public void PlayNormalBgm()
    {
        if (currentBgmState != BgmPlayingState.Normal)
        {
            currentBgmState = BgmPlayingState.Normal;
            IntroloopPlayer.Instance.Play(NormalBgm);
        }
    }

    public void StopBgm()
    {
        if (currentBgmState != BgmPlayingState.Stopped)
        {
            currentBgmState = BgmPlayingState.Stopped;
            IntroloopPlayer.Instance.Stop();
        }
    }
}
