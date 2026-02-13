using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorChange : MonoBehaviour
{
    [Header("Layer Tags (Red, Green, Blue)")]
    [SerializeField] private string redLayer;
    [SerializeField] private string greenLayer;
    [SerializeField] private string blueLayer;

    [Header("References")]
    [SerializeField] private Volume volume;
    [SerializeField] private RectTransform wheelVisual;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float colorLerpSpeed = 1f;
    [SerializeField] private float hybridDuration = 2f;

    private ColorAdjustments colorAdjustments;

    // 0 = Red, 1 = Green, 2 = Blue (counter-clockwise)
    private int baseIndex = 0;

    private bool isHybrid = false;
    private int hybridDirection; // -1 left, +1 right
    private float hybridTimer;

    private float currentAngle;
    private float targetAngle;

    private float colorLerp;
    private Color startColor;
    private Color targetColor;

    private readonly Color[] colors =
    {
        new Color(1f, 0.5f, 0.5f, 1f), // Red
        new Color(0.5f, 1f, 0.5f, 1f), // Green
        new Color(0.5f, 0.5f, 1f, 1f)  // Blue
    };

    private string[] layerTags;

    void Start()
    {
        if (volume == null)
        {
            Debug.LogWarning($"[{nameof(ColorChange)}] volume reference is null.");
        }
        else if (!volume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogWarning($"[{nameof(ColorChange)}] Volume profile has no ColorAdjustments.");
        }

        layerTags = new string[] { redLayer, greenLayer, blueLayer };

        currentAngle = wheelVisual != null ? wheelVisual.eulerAngles.z : 0f;
        targetAngle = AngleForIndex(baseIndex);

        ApplySingleState(baseIndex);
    }

    void Update()
    {
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Q))
            HandleInput(-1, shift);

        if (Input.GetKeyDown(KeyCode.E))
            HandleInput(+1, shift);

        UpdateHybridTimer();
        UpdateColor();
        UpdateRotation();
    }

    void HandleInput(int direction, bool shiftHeld)
    {
        if (isHybrid)
        {
            CommitFromHybrid(direction);
            return;
        }

        if (shiftHeld)
            StartHybrid(direction);
        else
            FullSwitch(direction);
    }

    void FullSwitch(int direction)
    {
        baseIndex = Wrap(baseIndex + direction);
        targetAngle = AngleForIndex(baseIndex);
        ApplySingleState(baseIndex);
    }

    void StartHybrid(int direction)
    {
        isHybrid = true;
        hybridDirection = direction;
        hybridTimer = hybridDuration;

        int neighbor = Wrap(baseIndex + direction);

        float aAng = AngleForIndex(baseIndex);
        float bAng = AngleForIndex(neighbor);
        targetAngle = ShortestMidpoint(aAng, bAng);

        ApplyHybridState(baseIndex, neighbor);
    }

    float ShortestMidpoint(float aAngle, float bAngle)
    {
        float delta = Mathf.DeltaAngle(aAngle, bAngle);
        float mid = aAngle + delta * 0.5f;
        return NormalizeAngle(mid);
    }

    float NormalizeAngle(float ang)
    {
        ang %= 360f;
        if (ang < 0f) ang += 360f;
        return ang;
    }

    void CommitFromHybrid(int inputDirection)
    {
        int neighbor = Wrap(baseIndex + hybridDirection);

        if (inputDirection == hybridDirection)
        {
            baseIndex = neighbor;
        }

        isHybrid = false;
        targetAngle = AngleForIndex(baseIndex);

        ApplySingleState(baseIndex);
    }

    void UpdateHybridTimer()
    {
        if (!isHybrid) return;

        hybridTimer -= Time.deltaTime;

        if (hybridTimer <= 0f)
        {
            isHybrid = false;
            targetAngle = AngleForIndex(baseIndex);
            ApplySingleState(baseIndex);
        }
    }

    void ApplySingleState(int index)
    {
        for (int i = 0; i < layerTags.Length; i++)
            SetLayerActive(layerTags[i], i == index);

        BeginColorTransition(colors[index]);
    }

    void ApplyHybridState(int a, int b)
    {
        for (int i = 0; i < layerTags.Length; i++)
            SetLayerActive(layerTags[i], i == a || i == b);

        BeginColorTransition((colors[a] + colors[b]) * 0.5f);
    }

    void BeginColorTransition(Color newTarget)
    {
        startColor = colorAdjustments != null ? colorAdjustments.colorFilter.value : Color.white;
        targetColor = newTarget;
        colorLerp = 0f;
    }

    void UpdateColor()
    {
        if (colorLerp < 1f && colorAdjustments != null)
        {
            colorLerp += Time.deltaTime * colorLerpSpeed;
            colorLerp = Mathf.Clamp01(colorLerp);

            colorAdjustments.colorFilter.value =
                Color.Lerp(startColor, targetColor, colorLerp);
        }
    }

    void UpdateRotation()
    {
        currentAngle = Mathf.MoveTowardsAngle(
            currentAngle,
            targetAngle,
            rotationSpeed * Time.deltaTime
        );

        if (wheelVisual != null)
            wheelVisual.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    float AngleForIndex(int index)
    {
        return index * -120f;
    }

    int Wrap(int value)
    {
        return (value + 3) % 3;
    }

    void SetLayerActive(string tag, bool active)
    {
        if (string.IsNullOrEmpty(tag))
            return;

        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (var obj in objects)
        {
            var sr = obj.GetComponent<SpriteRenderer>();
            var bc = obj.GetComponent<BoxCollider2D>();

            if (sr != null)
            {
                Color c = sr.color;
                c.a = active ? 1f : 0.2f;
                sr.color = c;
            }

            if (bc != null)
                bc.enabled = active;
        }
    }
}