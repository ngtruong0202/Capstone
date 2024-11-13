using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0;
    private int coinsToComplete = 5;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += CoinCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= CoinCollected;
    }
    
    private void CoinCollected()
    {
        if (coinsCollected < coinsToComplete)
        {
            coinsCollected++;
        }

        if(coinsCollected >= coinsToComplete)
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        throw new System.NotImplementedException();
    }
}
