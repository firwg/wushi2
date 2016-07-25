using UnityEngine;
using System.Collections;





/// <summary>
/// 对于相机的控制 关键在于三个参数， offset的position，相机的transform.Forward，还有fieldofworld
/// </summary>
public class CameraBehaviour : MonoBehaviour
{

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(lookAtPosition, 0.2f);
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawLine(transform.position, lookAtPosition);
    }


    //Player
    private Transform PlayerTransform;
    private BlackBoard PlayerBlackBoard;
    private CameraOffsetBehaviour CameraOffsetInPalyer;

    //Camera
    public Animation Animation;
    private Animation ParentAnimation;
    private Transform CameraTransform;
    public Vector3 lookAt;
    public Vector3 lookAtPosition;


    public static CameraBehaviour Instance;


    float DisabledTime = 0;
    float CurrentFovTime;
    float FovTime;
    float FovStart;
    float FovCameraEnd;
    float CurrentCameraFov = 0;
    bool FovCameraOk;

  /*  float CurrentBlurTime;
    float BlurTime;
    float BlurStart;
    float BlurEnd;
    float CurrentBlur;
    bool BlurOk = true;*/

    float CurrentShiftTime;
    float ShiftTime;
    float TimeScaleStart;
    float TimeScaleEnd;
    float TimeScaleCurrent;
    bool ShiftOk = false;
    
   // Vignetting CriticalHitEffect; 

    public float BaseFov;

    void Awake()
    {
        Instance = this;
        Animation = Camera.main.GetComponent<Animation>();
        ParentAnimation = GetComponent<Animation>();
        CameraOffsetInPalyer = GameObject.Find("Player").GetComponent<CameraOffsetBehaviour>();
        CameraTransform = transform;
        PlayerTransform = GameObject.Find("Player").transform;
        PlayerBlackBoard = GameObject.Find("Player").GetComponent<Agent>().BlackBoard;

    }

    void Start()
    {

   #region  hehe
   /*     CriticalHitEffect =  GetComponentInChildren<Vignetting>();
        CriticalHitEffect.blurVignette = 0;
        CriticalHitEffect.enabled = false;*/
  //      BlurOk = true;

        //AttTargetTransform = Target.GetComponent<Agent>().BlackBoard.DesiredTarget.transform;

        //  CameraTransform.position = Offset.GetCameraPosition();

        /*   Vector3 dir = CameraTransform.forward;
           dir.y = 0;
           dir.Normalize();
           Vector3 t = TargetTransform.position;
           t += dir * 1.5f;

           CameraTransform.LookAt(t);*/
   #endregion

        DisabledTime = 0;
        CurrentFovTime = 0;
        FovTime = 0;
        FovStart = 0;
        FovCameraEnd = 0;
        BaseFov = CurrentCameraFov = Camera.main.fieldOfView;
        FovCameraOk = true;
        TimeScaleCurrent = 1;
        ShiftOk = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Game.Instance.GameState == E_GameState.Game)
        {
            UpdateFov();
            UpdateSloMotion();
            //UpdatePPE();
        }

        if (DisabledTime >= Time.timeSinceLevelLoad)
            return;

        // Where should our camera be looking right now?鏇存柊鐩告満鎵�鍦ㄧ殑浣嶇疆
        Vector3 goalPosition = CameraOffsetInPalyer.GetCameraPosition();
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, goalPosition, Time.deltaTime * 4);


        //鏇存柊鐩告満鐨勬湞鍚?
        if (PlayerBlackBoard.DesiredTarget == null)
        {
            //Vector3 dir = CameraTransform.forward;
            //Debug.Log("CameraTransform.forward=" + dir);
            //dir.y = 0;
            //dir.Normalize();
            //Debug.Log("dir=" + dir);

            //Debug.Log("PlayerTransform.position=" + PlayerTransform.position);
            lookAtPosition = PlayerTransform.position;
            //t += dir * 0.7f;
        }
        else
        {
            Vector3 targetPos = PlayerBlackBoard.DesiredTarget.transform.position;


            if (GetComponent<Camera>() != null)
            {
                Vector3 pos = Camera.main.WorldToViewportPoint(targetPos);
                if (pos.x > 0.9F)
                    CameraOffsetInPalyer.OffSetMoveToLeftALittle();
                else if (pos.x < 0.1)
                    CameraOffsetInPalyer.OffSetMoveToRightALittle();
            }
            lookAtPosition = new Vector3((PlayerTransform.position.x+ targetPos.x)/2 , (PlayerTransform.position.y + targetPos.y) / 2,(PlayerTransform.position.z + targetPos.z) / 2);
        }

        //Debug.Log("t=" + t);
        lookAt = lookAtPosition - CameraTransform.position;
        lookAt.Normalize();



        //Debug.Log("lookAt="+ lookAt);

        CameraTransform.forward = Vector3.Lerp(CameraTransform.forward, lookAt, Time.deltaTime * 4);
    }



    public bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }


    public void PlayCameraAnim(string animName, bool overrideBehaviour, bool fade)
    {
        if (ParentAnimation[animName] == null)
            return;

        if (overrideBehaviour)
        {
            StartCoroutine(FadeInOutAndCameraPlay(animName));
        }
        else
        {
            ParentAnimation[animName].blendMode = AnimationBlendMode.Blend;
            ParentAnimation.CrossFade(animName, 0.5f);

            if (overrideBehaviour)
                DisabledTime = Time.timeSinceLevelLoad + ParentAnimation[animName].length;
        }
    }

    public void ComboShake(int comboLevel)
    {
        string[] animations = { "shakeCombo1", "shakeCombo2", "shakeCombo3" };

        if (Animation[animations[comboLevel]] == null)
            return;

        Animation[animations[comboLevel]].blendMode = AnimationBlendMode.Blend;
        Animation.Play(animations[comboLevel]);
    }

    public void BigInjuryShake()
    {
        if (Animation["shakeInjury"] == null)
            return;

        Animation["shakeInjury"].blendMode = AnimationBlendMode.Blend;
        Animation.Play("shakeInjury");
    }


    public void Reset()
    {
        CameraTransform.position = CameraOffsetInPalyer.GetCameraPosition();

        Vector3 dir = CameraTransform.forward;
        dir.y = 0;
        dir.Normalize();
        Vector3 t = PlayerTransform.position;
        t += dir * 0.7f;

        Vector3 lookAt = t - CameraTransform.position;
        lookAt.Normalize();
        CameraTransform.forward = lookAt;
    }

    public void Activate(Vector3 pos, Vector3 lookAt)
    {
//        Debug.Log(pos);
        DisabledTime = 0;

        CameraTransform.position = pos;
        CameraTransform.LookAt(lookAt);
    }

    public void InterpolateScaleFovBack()
    {
        CancelInvoke("InterpolateScaleFovBack");
        InterpolateTimeScale(1, 0.4f);
        InterpolateFov(BaseFov, 0.4f);

        Player.Instance.Agent.BlackBoard.Invulnerable = false;
   /*     if (CriticalHitEffect)
            CriticalHitEffect.enabled = false;*/
    }

    public void InterpolateFov(float newFov, float inTime)
    {
        CancelInvoke("InterpolateScaleFovBack");
        CurrentFovTime = 0;
        FovTime = inTime;
        FovStart= CurrentCameraFov;
        FovCameraEnd = newFov;

        FovCameraOk = false;
    }

    void UpdateFov()
    {
        if (FovCameraOk == false)
        {
            CurrentFovTime += Time.deltaTime                        ;

            if (CurrentFovTime > FovTime)
            {
                CurrentFovTime = FovTime;
                FovCameraOk = true;
            }

            if (CurrentFovTime >= 0)
                CurrentCameraFov = Mathfx.Hermite(FovStart, FovCameraEnd, CurrentFovTime / FovTime);
        }
        Camera.main.fieldOfView = CurrentCameraFov;
    }

    public void InterpolateTimeScale(float newTimeScale, float inTime)
    {
        CancelInvoke("InterpolateScaleFovBack");
  //      Debug.Log(Time.timeSinceLevelLoad + "scale" + newTimeScale + " " + inTime);
        CurrentShiftTime = 0;
        ShiftTime = inTime;
        TimeScaleStart = Time.timeScale;
        TimeScaleEnd = newTimeScale;

        ShiftOk = false;

        //if (CriticalHitEffect)
            //CriticalHitEffect.enabled = true;

        Player.Instance.Agent.BlackBoard.Invulnerable = true;
    }

    public void InterpolateBlur(float newBlur, float inTime)
    {
 /*       CurrentBlurTime = 0;
        BlurTime = inTime;
        BlurStart = CriticalHitEffect.blurVignette;
        BlurEnd = newBlur;

        BlurOk = false;*/
    }

   /* void UpdateCriticalPPE()
    {
        if (BlurOk == false)
        {
            CurrentBlurTime += Time.deltaTime;

            if (CurrentBlurTime > BlurTime)
            {
                CurrentBlurTime = BlurTime;
                BlurOk = true;
            }

            if (CurrentBlurTime >= 0)
                CurrentBlur = Mathfx.Lerp(BlurStart, BlurEnd, CurrentBlurTime / BlurTime);

            CriticalHitEffect.blurVignette = CurrentBlur;
            CriticalHitEffect.enabled = CriticalHitEffect.blurVignette != 0;
        }
    }*/

    void UpdateSloMotion()
    {
        if (ShiftOk == false)
        {
            CurrentShiftTime += Time.deltaTime;

            if (CurrentShiftTime > ShiftTime)
            {
                CurrentShiftTime = ShiftTime;
                ShiftOk = true;
            }

            if (CurrentShiftTime >= 0)
                TimeScaleCurrent = Mathfx.Hermite(TimeScaleStart, TimeScaleEnd, CurrentShiftTime / ShiftTime);
        }
        Time.timeScale = TimeScaleCurrent;
    }

    IEnumerator FadeInOutAndCameraPlay(string animName)
    {
        GuiManager.Instance.FadeOut(0.1f, 1);
        yield return new WaitForSeconds(0.2f);

        ParentAnimation[animName].blendMode = AnimationBlendMode.Blend;
        ParentAnimation.CrossFade(animName, 0.5f);

        DisabledTime = Time.timeSinceLevelLoad + ParentAnimation[animName].length;

        StartCoroutine(FadeInOutAndCameraPlayEnd(ParentAnimation[animName].length - 0.5f));

        yield return new WaitForSeconds(0.1f);

        GuiManager.Instance.FadeIn();
    }

    IEnumerator FadeInOutAndCameraPlayEnd(float delay)
    {
        yield return new WaitForSeconds(delay);

        GuiManager.Instance.FadeOut(0.3f, 1);
        yield return new WaitForSeconds(0.5f);
        GuiManager.Instance.FadeIn();
    }
}

