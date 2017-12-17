using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    static public SoundManager instance;
    static public SoundManager Instance
    {
        get { return instance; }
        private set
        {
            if (!instance)
            {
                instance = (SoundManager)GameObject.FindObjectOfType(typeof(SoundManager));
                if (!instance)
                {
                    GameObject instanceObject = new GameObject();
                    instanceObject.name = "SoundManager";
                    instance = (SoundManager)instanceObject.AddComponent(typeof(SoundManager));
                }
            }

        }
    }
    [Header("Sound Setting")]
    [Range(0.0f, 1.0f)]
    public float bgmVolume = 1.0f;
    [Range(0.0f, 1.0f)]
    public float effectVolume = 1.0f;
    public bool isBgmMute = false;
    public bool isEffectMute = false;

    [Header("Sound Resource")]
    public AudioClip[] bgmSounds;
    public AudioClip[] effectSounds;

    private GameObject bgmPlayObject;

    private SoundWaveData data;
    private GameObject SoundWaveManager;
    private GameObject SoundWave;


    private void Start()
    {
        if (!instance)
        {
            instance = (SoundManager)GameObject.FindObjectOfType(typeof(SoundManager));
            if (!instance)
            {
                GameObject instanceObject = new GameObject();
                instanceObject.name = "SoundManager";
                instance = (SoundManager)instanceObject.AddComponent(typeof(SoundManager));
            }
        }

        if (bgmPlayObject == null)
        {
            CreateBGMObject();
        }
        SetDoorSoundData();
    }

    private void CreateBGMObject()
    {
        GameObject obj = new GameObject();
        obj.AddComponent<AudioSource>();

        obj.name = "bgm_main";
        obj.transform.position = transform.position;
        obj.transform.SetParent(transform);

        bgmPlayObject = obj;
    }

    public void PlayEffectSfx(AudioClip _sfx, Vector3 _pos, float _maxDist = 30, float _minDist = 10)
    {
        GameObject obj = new GameObject();
        AudioSource aud = obj.AddComponent<AudioSource>();

        obj.name = "effect_"+_sfx.name;
        obj.transform.position = _pos;
        obj.transform.SetParent(transform);

        aud.clip = _sfx;
        aud.minDistance = _minDist;
        aud.maxDistance = _maxDist;
        aud.volume = effectVolume;
        aud.mute = isEffectMute;

        aud.Play();

        data.circlePoint = _pos;

        Destroy(obj, _sfx.length);
    }

    public void PlayBGM(AudioClip bgmClip, Vector3 _pos, float _maxDist = 30, float _minDist = 10)
    {
        AudioSource aud = bgmPlayObject.GetComponent<AudioSource>();

        if (aud == null)
            aud = bgmPlayObject.AddComponent<AudioSource>();

        if (aud.isPlaying)
            aud.Stop();

        aud.clip = bgmClip;
        aud.minDistance = _minDist;
        aud.maxDistance = _maxDist;
        aud.volume = bgmVolume;
        aud.mute = isBgmMute;
        aud.loop = true;

        aud.Play();
    }

    public void AllPauseSound()
    {
        foreach (AudioSource child in transform.GetComponentsInChildren<AudioSource>())
        {
            child.Pause();
        }
    }

    public void AllUnPauseSound()
    {
        foreach (AudioSource child in transform.GetComponentsInChildren<AudioSource>())
        {
            child.UnPause();
        }
    }

    public void AllStopSound()
    {
        foreach (AudioSource child in transform.GetComponentsInChildren<AudioSource>())
        {
            child.Stop();
        }
    }

    public void ChangeSoundSetting()
    {
        foreach (AudioSource child in transform.GetComponentsInChildren<AudioSource>())
        {
            child.mute = isEffectMute;
            child.volume = effectVolume;
        }

        AudioSource source = bgmPlayObject.GetComponent<AudioSource>();

        if (!source) source = bgmPlayObject.AddComponent<AudioSource>();

        source.mute = isBgmMute;
        source.volume = bgmVolume;
    }

    // 사운드 설정의 Get, Set
    public void SetEffectVolume(float _volume)
    {
        if (_volume < 0 || _volume > 1) return;

        effectVolume = _volume;
    }

    public void SetBGMVolume(float _volume)
    {
        if (_volume < 0 || _volume > 1) return;

        bgmVolume = _volume;
    }

    public void SetEffectMute(bool _mute)
    {
        isEffectMute = _mute;
    }

    public void SetBGMMute(bool _mute)
    {
        isBgmMute = _mute;
    }


    public void FlagChangePlayer()
    {
        data.isEnemy = 0.0f;
    }
    public void FlagChangeMurderer()
    {
        data.isEnemy = 1.0f;
    }


    public void ActiveSoundWave()
    {
        Debug.Log("액티브");
        SoundWave = SoundWaveManager.GetComponent<SoundWaveManager>().SearchInactiveObject();
        SoundWave.GetComponent<SoundWaveSphere>().SetTakeData(data);
    }

    public void SetDoorSoundData()
    {
        data.isActive = 1.0f;
        data.isEnemy = 0.0f;
        data.radius = 0.5f;
        data.subRadius = 0.6f;
       
        SoundWaveManager = GameObject.Find("SoundWaveManager");
    }
}
