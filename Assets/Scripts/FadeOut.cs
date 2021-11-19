using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public SpriteRenderer start;
    public float timerLength;
    private bool fadeout = false;
    private float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        fadeout = false;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var scale = transform.localScale;
        if (scale.x > 1 && scale.y > 1)
        {
            if (_timer > timerLength)
            {
                fadeout = true;
            }
            else
            {
                Time.timeScale = 1;
                _timer += Time.deltaTime;
                return;
            }
        }
        if (fadeout)
        {
            transform.localScale = scale - new Vector3(0.1f, 0.1f, 0.1f);
        }
        else
        {
            transform.localScale = scale + new Vector3(0.1f, 0.1f, 0.1f);
        }

        if (scale.x < 0 && scale.y < 0)
        {
            Destroy(gameObject);
        }


    }
}
