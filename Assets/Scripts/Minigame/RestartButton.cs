using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public MinigameCore minigameCore;
    public void Restart()
    {
        minigameCore.Mission3Start();
    }
}
