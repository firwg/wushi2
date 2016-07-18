using UnityEngine;using System.Collections;
public abstract class InteractionGameObject : MonoBehaviour{

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

    [System.NonSerialized]    public E_InteractionObjects InteractionType = E_InteractionObjects.None;

    public GameObject[] ObjectsToShow;
    public GameObject[] ObjectsToHide;
    public InteractionParticle[] Emitters;
    public InteractionSound[] Sounds;

    public ParticleEmitter ActiveEffect;
    public AnimationClip CameraAnimation;    public abstract Transform GetEntryTransform();

    public  Vector3 Position { get {return transform.position;}}    public abstract float DoInteraction(E_InteractionType interaction);

    protected bool InteractionObjectUsable = true;
    protected bool Enabled = true;

    public bool IsActive { get { return InteractionObjectUsable; } }
    public bool IsEnabled { get { return Enabled; } }

    public void Enable(bool enable)
    {
        Enabled = enable;
        gameObject.SetActiveRecursively(enable);

        if (ActiveEffect)
            ActiveEffect.emit = enable;

    //    Debug.Log(name + enable);
    }

    protected void OnInteractionStart()
    {
        if (ActiveEffect)
            ActiveEffect.emit = false;

        if (CameraAnimation)
            CameraBehaviour.Instance.PlayCameraAnim(CameraAnimation.name, true, true);

        for (int i = 0; Emitters != null && i < Emitters.Length; i++)
        {
            Mission.Instance.StartCoroutine(ParticleRun(Emitters[i].Emitter, Emitters[i].Delay));
            Mission.Instance.StartCoroutine(ParticleStop(Emitters[i].Emitter, Emitters[i].Delay + Emitters[i].Life));

            if (Emitters[i].LinkOnRoot)
                Emitters[i].Emitter.transform.parent = GetEntryTransform();
        }

        for (int i = 0; Sounds != null && i < Sounds.Length; i++)
        {
            Mission.Instance.StartCoroutine(SoundRun(Sounds[i].Audio, Sounds[i].Delay));
            Mission.Instance.StartCoroutine(SoundStop(Sounds[i].Audio, Sounds[i].Delay + Sounds[i].Life));

            if (Sounds[i].Parent)
                Sounds[i].Audio.transform.parent = Sounds[i].Parent;
        }

        for (int i = 0; ObjectsToShow != null && i < ObjectsToShow.Length; i++)
            ObjectsToShow[i].SetActiveRecursively(true);
    }

    protected void OnInteractionEnd()
    {
        for (int i = 0;ObjectsToHide != null && i < ObjectsToHide.Length; i++)
            ObjectsToHide[i].SetActiveRecursively(false);

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


    public virtual void Restart()
    {
         for (int i = 0; ObjectsToShow != null && i < ObjectsToShow.Length; i++)
            ObjectsToShow[i].SetActiveRecursively(false);

        for (int i = 0;ObjectsToHide != null && i < ObjectsToHide.Length; i++)
            ObjectsToHide[i].SetActiveRecursively(true);

        InteractionObjectUsable = true;
    }

}