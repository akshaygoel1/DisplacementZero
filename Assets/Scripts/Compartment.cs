using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compartment : MonoBehaviour
{
    public SpriteRenderer topLayer;
    bool isInCompartment = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fader(-1));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fader(1));
        }
    }

    IEnumerator Fader(int multiplier)
    {
        if(multiplier == 1)
        {
            while(!Mathf.Approximately(topLayer.color.a,1f))
            {
                yield return new WaitForSeconds(0.01f);
                topLayer.color = new Color(topLayer.color.r, topLayer.color.g, topLayer.color.b, topLayer.color.a + 0.1f);
                if (topLayer.color.a > 0.95f)
                {
                    topLayer.color = new Color(topLayer.color.r, topLayer.color.g, topLayer.color.b, 1f);
                    yield break;
                }
            }
        }
        else
        {
            while (!Mathf.Approximately(topLayer.color.a, 0f))
            {
                yield return new WaitForSeconds(0.01f);
                topLayer.color = new Color(topLayer.color.r, topLayer.color.g, topLayer.color.b, topLayer.color.a - 0.1f);
                if (topLayer.color.a < 0.1f)
                {
                    topLayer.color = new Color(topLayer.color.r, topLayer.color.g, topLayer.color.b, 0f);
                    yield break;
                }
            }
        }
    }
}
