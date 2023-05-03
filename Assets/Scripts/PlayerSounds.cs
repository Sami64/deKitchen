using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;

    private float footstepTimer;

    private float footstepTimerMax = .1f;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            if(player.IsWalking())
            {
                footstepTimer = footstepTimerMax;

                SoundManager.Instance.PlayFootstepsSound(player.transform.position);
            }
        }
    }
}
