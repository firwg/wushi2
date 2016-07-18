using UnityEngine;
using System.Collections;

public abstract class BreakableObject : MonoBehaviour
{

    [System.Serializable]
    public class InteractionParticle
    {
        public ParticleEmitter Emitter;
        public float Delay;
        public float Life;
        public bool LinkOnRoot;

    }

    [System.Serializable]
    public class InteractionSound
    {
        public AudioSource Audio;
        public float Delay;
        public float Life;
        public Transform Parent;
    }

    public GameObject BreakableGameObject;
    public AnimationClip AnimBreak;
    public InteractionParticle[] Emitters;
    public InteractionSound[] Sounds;

    protected bool Active = true;
    protected Transform Root;
    private Animation Animation;
    private GameObject GameObject;


    public bool IsActive { get { return Active; } }
    public Vector3 Position { get { return Root.position; } }


    public void Initialize()
    {
        GameObject = gameObject;
        Root = transform;
        Animation = BreakableGameObject.GetComponent<Animation>();
        Animation.wrapMode = WrapMode.Once;

       // Debug.Log(Time.timeSinceLevelLoad + " " + gameObject.name + " Initialize");
    }



    public virtual void Break()
    {
        //Debug.Log(Time.timeSinceLevelLoad + " " + gameObject.name + " break");
        Active = false;
        Animation.Play(AnimBreak.name);

        float end = Animation[AnimBreak.name].length;
        
        for (int i = 0; Emitters != null && i < Emitters.Length; i++)
        {
            Mission.Instance.StartCoroutine(ParticleRun(Emitters[i].Emitter, Emitters[i].Delay));
            Mission.Instance.StartCoroutine(ParticleStop(Emitters[i].Emitter, Emitters[i].Delay + Emitters[i].Life));

            if (end < Emitters[i].Delay + Emitters[i].Life)
                end = Emitters[i].Delay + Emitters[i].Life;

            if (Emitters[i].LinkOnRoot)
                Emitters[i].Emitter.transform.parent = Root;
        }

        for (int i = 0; Sounds != null && i < Sounds.Length; i++)
        {
            Mission.Instance.StartCoroutine(SoundRun(Sounds[i].Audio, Sounds[i].Delay));
            Mission.Instance.StartCoroutine(SoundStop(Sounds[i].Audio, Sounds[i].Delay + Sounds[i].Life));

            if (end < Sounds[i].Delay + Sounds[i].Life)
                end = Sounds[i].Delay + Sounds[i].Life;

            if (Sounds[i].Parent)
                Sounds[i].Audio.transform.parent = Sounds[i].Parent;
        }

        Invoke("OnDone", end +0.1f);
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnDone()
    {
        GameObject.SetActiveRecursively(false);
        BreakableGameObject.SetActiveRecursively(false);
    }

    public virtual void Restart()
    {
        Animation.Stop();
        AnimBreak.SampleAnimation(BreakableGameObject, 0);

        Active = true;
        //gameObject.SetActiveRecursively(true);
    }

    public void Enable()
    {
        GameObject.SetActiveRecursively(true);
        BreakableGameObject.SetActiveRecursively(true);

        //Debug.Log(Time.timeSinceLevelLoad + " " + gameObject.name + " Enable");
    }

    public void Disable()
    {
        GameObject.SetActiveRecursively(false);
        BreakableGameObject.SetActiveRecursively(false) ;


        //Debug.Log(Time.timeSinceLevelLoad + " " + gameObject.name + " Disable");

    }


    private IEnumerator ParticleRun(ParticleEmitter emitter,  float delay)
    {
        yield return new WaitForSeconds(delay);
        emitter.emit = true;

        //Debug.Log(Time.timeSinceLevelLoad + " ParticleRun"); 
    }

    private IEnumerator ParticleStop(ParticleEmitter emitter, float delay)
    {
        yield return new WaitForSeconds(delay);
        emitter.emit = false;

       // Debug.Log(Time.timeSinceLevelLoad + " ParticleStop");
    }

    private IEnumerator SoundRun(AudioSource audio, float delay)
    {
        yield return new WaitForSeconds(delay);
        audio.Play();
        //Debug.Log(Time.timeSinceLevelLoad + " " + audio.name + " audio start");
    }

    private IEnumerator SoundStop(AudioSource audio, float delay)
    {
        yield return new WaitForSeconds(delay);
        audio.Stop();
        //Debug.Log(Time.timeSinceLevelLoad + " " + audio.name + " audio stop ");
    }
}

