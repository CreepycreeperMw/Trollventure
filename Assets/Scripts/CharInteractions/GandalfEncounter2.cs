using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GandalfEncounter2 : MonoBehaviour
{
    [SerializeField] private GameObject gandalf;
    [SerializeField] private GameObject con;
    private Animator anim;
    private bool triggered = false;
    private Vector3 offset = new Vector3(-3f, 1.4f);

    private void Start()
    {
        anim = gandalf.GetComponent<Animator>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("player"))
        {
            triggered = true;
            PlayerMovement.blockMovement = true;
            PlayerMovement.pl.rb.velocity = Vector2.zero;

            MusicHandler.handler.Fade(2f, 0f, 0.2f);
            gandalf.SetActive(true);

            FadeObject fade = gandalf.GetComponent<FadeObject>();
            fade.StartFade(3f, 0f, 1f, 0.2f);

            yield return new WaitForSeconds(3f);

            TextBubble tb = TextBubble.Create(con, "", offset);
            tb.WriteText("Hmm...", 0.4f, offset);
            yield return new WaitUntil(() => !tb.isWriting);
            yield return new WaitForSeconds(1.2f);

            tb.WriteText("Applaus", 0.4f, offset);
            yield return new WaitUntil(() => !tb.isWriting);
            yield return new WaitForSeconds(0.8f);
            tb.AddText(", fast unterdurchschnittlich", 1f, offset);
            yield return new WaitUntil(() => !tb.isWriting);
            yield return new WaitForSeconds(2f);

            // tb.Setup("Fast unterdurchschnittlich...", offset);
            tb.Setup("Das ist ein Portal", offset);
            yield return new WaitForSeconds(2f);
            tb.Setup("Geh durch...", offset);
            yield return new WaitForSeconds(2f);
            tb.Setup("Aber nicht zu langsam!", offset);
            yield return new WaitForSeconds(2f);
            tb.Delete();

            fade.StartFade(0.5f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.4f);

            PlayerMovement.blockMovement = false;

            MusicHandler.handler.Fade(1f, 0f, 1f);
        }
    }
}
