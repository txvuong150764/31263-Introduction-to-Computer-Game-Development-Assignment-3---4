using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animatorController;

    [SerializeField]
    private GameObject player;
    private PlayerTween activeTween;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AddPlayerTween();
        if (activeTween != null)
        {
            checkDirection(); // Controls animator

            float time = (Time.time - activeTween.StartTime) / activeTween.Duration;
            float timeFraction = time * time * time;
        
            float dist = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);

            if (dist > 0.1f)
            {
                activeTween.Target.transform.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
            }
            if (dist < 0.1f)
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
            }

        }

        pos = player.transform.position; // Get position of player
    }

    public void AddPlayerTween()
    {
        if (activeTween == null)
        {
            if (pos.x == -7f && pos.y == 13f) // Move Down
            {
                activeTween = new PlayerTween(player.transform, player.transform.position, new Vector3(-7f, 9f, 0.0f), Time.time, 1.5f);
            }
            if (pos.x == -7f && pos.y == 9f) // Move Left
            {
                activeTween = new PlayerTween(player.transform, player.transform.position, new Vector3(-12f, 9f, 0.0f), Time.time, 1.5f);
            }

            if (pos.x == -12f && pos.y == 9f) // Move Up
            {
                activeTween = new PlayerTween(player.transform, player.transform.position, new Vector3(-12f, 13f, 0.0f), Time.time, 1.5f);
            }

            if (pos.x == -12f && pos.y == 13) // Move Right
            {
                activeTween = new PlayerTween(player.transform, player.transform.position, new Vector3(-7f, 13f, 0.0f), Time.time, 1.5f);
            }
        }
    }

    public void checkDirection()
    {
        if (pos.y > activeTween.EndPos.y) // Down
        {
            animatorController.SetInteger("Direction", 3);
        }

        if (pos.x > activeTween.EndPos.x) // Left
        {
            animatorController.SetInteger("Direction", 0);
        }

        if (pos.y < activeTween.EndPos.y) // Up
        {
            animatorController.SetInteger("Direction", 1);
        }

        if (pos.x < activeTween.EndPos.x) // Right
        {
            animatorController.SetInteger("Direction", 2);
        }
    }
}
