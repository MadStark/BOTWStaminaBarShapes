using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaTest : MonoBehaviour
{
    public StaminaUI ui;
    public float max = 1;
    public float recoverySpeed = 0.1f;
    public float current;
    public float sprintEffort = 0.1f;
    public Animator animator;


    private bool recovering;

    private void Start()
    {
        current = max;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
            max += 0.2f;
        else if (Input.GetKeyDown(KeyCode.Comma))
            max -= 0.2f;

        if (!recovering && Input.GetKey(KeyCode.LeftShift))
        {
            current -= sprintEffort * Time.deltaTime;
            ui.consumption = sprintEffort * 0.3f;

            if (current <= 0)
                recovering = true;

            animator.SetBool("Running", true);
        }
        else
        {
            current += recoverySpeed * Time.deltaTime;
            ui.consumption = 0f;

            animator.SetBool("Running", false);
        }

        if (recovering && current >= 1f)
            recovering = false;

        current = Mathf.Clamp(current, 0, max);

        ui.maxFill = max;
        ui.fillAmount = current;
        ui.recovering = recovering;
    }
}
