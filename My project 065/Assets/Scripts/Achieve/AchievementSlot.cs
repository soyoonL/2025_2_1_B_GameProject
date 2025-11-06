using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementSlot : MonoBehaviour
{
    [Header("UI 레퍼런스")]
    public Image iconImage;
    public Text nameText;
    public Text descriptionText;
    public Text progressText;
    public Slider progressSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAchievement(AchievementData achievement, float progress)
    {
        if (nameText != null) // 텍스트 설정
            nameText.text = achievement.achievmentName;

        if (descriptionText != null)
            descriptionText.text = achievement.description;

        if (iconImage != null && achievement.icon != null) // 아이콘 설정
            iconImage.sprite = achievement.icon;

        if (progressSlider != null) // 진행 표시
            progressSlider.value = achievement.isUnlocked ? 1f : progress;

        if (progressText != null)
        {
            if (achievement.isUnlocked)
            {
                progressText.text = "완료!";
            }
            else
            {
                int current = Mathf.FloorToInt(progress * achievement.requiredAmount);
                progressText.text = current + "/" + achievement.requiredAmount;
            }
        }
    }
}
