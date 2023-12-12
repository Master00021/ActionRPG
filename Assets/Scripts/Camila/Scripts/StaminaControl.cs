using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaControl : MonoBehaviour
{
    public PlayerController playercontroller;

    public float playerstamina = 100f;

    public float maxst = 100f;
    private float actioncost = 25;

    public bool regenerated = true;
    public bool sprinting = false;


    private float value = 15f;

    //ui
    [SerializeField] private Image staminaprogress = null;
    [SerializeField] private CanvasGroup slider = null;

    private void Update()
    {
        if (!sprinting)
        {
            if(playerstamina <= maxst - 0.01)
            {
                playerstamina += value * Time.deltaTime;
                UpdateStamina(1);

                if(playerstamina >= maxst)
                {
                    
                    slider.alpha = 0;
                    regenerated = true; 
                }
            }
        }
    }


    //Se puede volver a correr solo cuando se regenera al máximo la stamina
    public void SprintingMove()
    {
        if(regenerated == true)
        {
            sprinting = true;
            playercontroller.Run();
            playerstamina -= value * Time.deltaTime;
            UpdateStamina(1);

            if (playerstamina <= 0)
            {
                regenerated = false;

                slider.alpha = 0;
            }
        }
    }

    public void ActionCost()
    {
        if(playerstamina >= (maxst * actioncost / maxst))
            {
            playerstamina -= actioncost;
            playercontroller.Block();
            playercontroller.Dash();
            UpdateStamina(1);
            }
    }

    void UpdateStamina(int cost)
    {
        staminaprogress.fillAmount = playerstamina / maxst;

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
