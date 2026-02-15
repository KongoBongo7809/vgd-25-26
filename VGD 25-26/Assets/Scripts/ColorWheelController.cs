using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorWheelController : MonoBehaviour
{
    [Header("Layer Tags")]
    [SerializeField] private string redTag;
    [SerializeField] private string greenTag;
    [SerializeField] private string blueTag;

    [Header("References")]
    [SerializeField] private Volume volume;
    [SerializeField] private RectTransform wheelTransform;
    [SerializeField] private Image hybridCooldownIndicator;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float colorBlendSpeed = 2f;
    [SerializeField] private float hybridDuration = 2f;
    [SerializeField] private float hybridCooldown = 4f;
    [SerializeField] private float hybridExitSpeed = 720f;

    private ColorAdjustments colorAdjustments;
    private readonly Color[] palette = {
        new Color(1f, 0.5f, 0.5f, 1f),
        new Color(0.5f, 1f, 0.5f, 1f),
        new Color(0.5f, 0.5f, 1f, 1f)
    };

    private string[] tags;
    private int baseIndex;
    private bool hybridActive;
    private bool hybridExitPhase;
    private int hybridDir;
    private float hybridTimer;
    private float cooldownTimer;
    private float currentAngle;
    private float targetAngle;
    private float indicatorAngle;
    private float colorT;
    private Color colorFrom;
    private Color colorTo;
    private const float FULL_STEP = -120f;
    private const float HALF_STEP = -60f;

    void Start()
    {
        tags = new[] { redTag, greenTag, blueTag };
        if (volume != null) volume.profile.TryGet(out colorAdjustments);
        currentAngle = baseIndex * FULL_STEP;
        targetAngle = currentAngle;
        indicatorAngle = 0f;
        ApplySingle(baseIndex);
    }

    void Update()
    {
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Input.GetKeyDown(KeyCode.Q)) ProcessInput(-1, shift);
        if (Input.GetKeyDown(KeyCode.E)) ProcessInput(+1, shift);
        HybridTick();
        ColorTick();
        RotationTick();
        UpdateIndicator();
    }

    void ProcessInput(int dir, bool shift)
    {
        if (hybridActive)
        {
            CommitFromHybrid(dir);
            return;
        }

        if (shift)
        {
            if (cooldownTimer < hybridCooldown) return;
            BeginHybrid(dir);
        }
        else
        {
            baseIndex = Wrap(baseIndex + dir);
            targetAngle += dir * FULL_STEP;
            ApplySingle(baseIndex);
        }
    }

    void BeginHybrid(int dir)
    {
        hybridActive = true;
        hybridDir = dir;
        cooldownTimer = 0f;
        int neighbor = Wrap(baseIndex + dir);
        targetAngle += dir * HALF_STEP;
        ApplyHybrid(baseIndex, neighbor);
    }

    void CommitFromHybrid(int inputDir)
    {
        int neighbor = Wrap(baseIndex + hybridDir);
        if (inputDir == hybridDir)
        {
            baseIndex = neighbor;
            targetAngle += inputDir * HALF_STEP;
        }
        else
        {
            targetAngle -= hybridDir * HALF_STEP;
        }
        hybridActive = false;
        hybridExitPhase = true;
        hybridTimer = hybridDuration;
        ApplySingle(baseIndex);
    }

    
    void HybridTick()
    {
        if (hybridActive)
        {
            hybridTimer -= Time.deltaTime;
            indicatorAngle = hybridTimer / hybridDuration * 180f;
            if (hybridTimer < 0f)
            {
                hybridActive = false;
                hybridTimer = hybridDuration;
                targetAngle = NearestEquivalent(baseIndex * FULL_STEP);
                indicatorAngle = 0f;
                ApplySingle(baseIndex);
            }
        }
        else if (cooldownTimer < hybridCooldown)
        {
            if (hybridExitPhase)
            {
                indicatorAngle = Mathf.MoveTowards(indicatorAngle, 0f, hybridExitSpeed * Time.deltaTime);
                if (indicatorAngle == 0f) hybridExitPhase = false;
            }
            else
            {
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer > hybridCooldown) cooldownTimer = hybridCooldown;
                indicatorAngle = cooldownTimer / hybridCooldown * 180f;
            }
        }
    }

    float NearestEquivalent(float desiredBaseAngle)
    {
        float k = Mathf.Round((currentAngle - desiredBaseAngle) / 360f);
        return desiredBaseAngle + k * 360f;
    }

    void ApplySingle(int index)
    {
        for (int i = 0; i < tags.Length; i++) ToggleTag(tags[i], i == index);
        StartColorTransition(palette[index]);
    }

    void ApplyHybrid(int a, int b)
    {
        for (int i = 0; i < tags.Length; i++) ToggleTag(tags[i], i == a || i == b);
        StartColorTransition((palette[a] + palette[b]) * 0.5f);
    }

    void StartColorTransition(Color to)
    {
        colorFrom = colorAdjustments != null ? colorAdjustments.colorFilter.value : Color.white;
        colorTo = to;
        colorT = 0f;
    }

    void ColorTick()
    {
        if (colorAdjustments == null) return;
        if (colorT >= 1f) return;
        colorT = Mathf.Clamp01(colorT + Time.deltaTime * colorBlendSpeed);
        colorAdjustments.colorFilter.value = Color.Lerp(colorFrom, colorTo, colorT);
    }

    void RotationTick()
    {
        float delta = targetAngle - currentAngle;
        float dist = Mathf.Abs(delta);
        float multiplier = 1f + Mathf.Clamp(dist / 180f, 0f, 3f);
        float step = rotationSpeed * multiplier * Time.deltaTime;
        currentAngle = Mathf.MoveTowards(currentAngle, targetAngle, step);
        if (wheelTransform != null) wheelTransform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    void UpdateIndicator()
    {
        if (hybridCooldownIndicator == null) return;
        hybridCooldownIndicator.fillAmount = Mathf.Clamp01(indicatorAngle / 360f);
    }

    int Wrap(int v)
    {
        v %= 3;
        if (v < 0) v += 3;
        return v;
    }

    void ToggleTag(string tag, bool on)
    {
        if (string.IsNullOrEmpty(tag)) return;
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var o in objs)
        {
            var sr = o.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = on ? 1f : 0.25f;
                sr.color = c;
            }
            var bc = o.GetComponent<BoxCollider2D>();
            if (bc != null) bc.enabled = on;
        }
    }
}