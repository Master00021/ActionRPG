using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float playerSt = 100f;
    private float maxSt = 100f;
    private float actionCost = 20f;

    public bool regenerated = true;

    public bool isSprinting = false;


    private float value = 2f;
    private float regSt = 1f;

    //staminabar
    private Image staminaProgress = null;
    private CanvasGroup slider = null;

    public PlayerController playerControl;

    //sprint
    private float slowed = 5;
    private float normal = 10;


    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSprinting == false)
        {
            if (playerSt <= maxSt - 0.01)
            {
                playerSt += regSt * Time.deltaTime;
                HighStamina(1);

                if (playerSt >= maxSt)
                {
                    playerControl.SprintSpeed(normal);
                    slider.alpha = 0;
                    regenerated = true;
                }
            }
        }
    }

    public void Sprinting()
    {
        if(regenerated == true)
        {
            playerControl.Run();
            isSprinting = true;
            playerSt -= value * Time.deltaTime;
            HighStamina(1);

            if(playerSt <= 0)
            {
                regenerated = false;
                playerControl.SprintSpeed(slowed);
                slider.alpha = 0;
            }
        }
    }
    public void StaminaCost()
    {
        if(playerSt >= (maxSt * actionCost / maxSt))
        {
            playerSt -= actionCost;

            HighStamina(1);
        }
    }

    void HighStamina(int cost)
    {
        staminaProgress.fillAmount = playerSt / maxSt;

        if(cost == 0)
        {
            slider.alpha = 0;
        }
        else
        {
            slider.alpha = 1;
        }
    }
}
