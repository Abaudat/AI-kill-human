using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiEasterEgg : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Animator animator;
    public Transform iconTransform;

    private AchievementTooltip tooltip;

    private Achievement achievement;
    private bool isHovering = false;
    private float hoverTime = 0;
    private float animationOffset;

    private void Awake()
    {
        tooltip = FindObjectOfType<AchievementTooltip>();
        animationOffset = Random.value * Mathf.PI * 2;
    }

    public void Populate(Achievement achievement)
    {
        this.achievement = achievement;
        icon.sprite = achievement.icon;
        if (achievement.isUnlocked)
        {
            animator.SetTrigger("Unlock");
        }
    }

    private void Update()
    {
        if (isHovering)
        {
            hoverTime += Time.deltaTime;
            TiltToMouse();
        }
        else
        {
            AnimateRotation();
        }
        if (hoverTime > 0.5f)
        {
            tooltip.PopulateAndMove(achievement.achivementName,
                achievement.isUnlocked ? achievement.achivementDescription : "???",
                achievement.icon,
                achievement.isUnlocked,
                GetComponent<RectTransform>());
            tooltip.Show();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        hoverTime = 0;
        transform.rotation = Quaternion.identity;
        tooltip.Hide();
    }

    private void TiltToMouse()
    {
        Vector2 delta = Input.mousePosition - transform.position;
        Vector2 normalizedDelta = 40 * delta / GetComponent<RectTransform>().rect.size;
        iconTransform.rotation = Quaternion.Euler(-normalizedDelta.y, normalizedDelta.x, 0);
    }

    private void AnimateRotation()
    {
        float rotationX = 20 * Mathf.Cos(Time.time + animationOffset);
        float rotationY = 20 * Mathf.Sin(Time.time + animationOffset);

        iconTransform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
