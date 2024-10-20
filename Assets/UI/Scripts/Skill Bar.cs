using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public int MaxValue;
    public Image fill;
    private bool skillused;
    private Animator playerAnimator;

    public int CurrentValue;

    void Start()
    {
        CurrentValue = 0;
        fill.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentValue == MaxValue)
        {
            TriggerSkillAnimation();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Add(3);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (CurrentValue == MaxValue)
            {
                Deduct(100);
                skillused = true;
            }
            skillused = false;
        }
       
    }

    public void Add(int i)
    {
        CurrentValue += i;

        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }

        fill.fillAmount = (float)CurrentValue / MaxValue;
    }

    public void Deduct(int i)
    {
        CurrentValue -= i;

        if (CurrentValue < 0)
        {
            CurrentValue = 0;
        }

        fill.fillAmount = (float)CurrentValue / MaxValue;
    }

    private void TriggerSkillAnimation()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("SkillReady"); 
        }
    }
}
