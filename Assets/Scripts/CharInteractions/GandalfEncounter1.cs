using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GandalfEncounter1 : MonoBehaviour
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
        if(!triggered && collision.gameObject.CompareTag("player"))
        {
            triggered = true;
            PlayerMovement.blockMovement = true;

            MusicHandler.handler.Fade(2f, 0f, 0.2f);
            gandalf.SetActive(true);

            FadeObject fade = gandalf.GetComponent<FadeObject>();
            fade.StartFade(3f, 0f, 1f, 0.2f);

            yield return new WaitForSeconds(4f);
            anim.SetBool("isHitting", true);
            yield return new WaitForSeconds(0.34f);

            AudioManager.instance.Play("hit");

            if (!GameHandler.watchedID1)
            {
                yield return new WaitForSeconds(0.71f);
                TextBubble tb = TextBubble.Create(con, "Wer bist du?", offset);
                yield return new WaitForSeconds(2f);
                tb.Setup("Was suchst du hier? ...", offset);
                tb.SetText("Was suchst du hier?");
                yield return new WaitForSeconds(0.4f);
                tb.SetText("Was suchst du hier? .");
                yield return new WaitForSeconds(0.4f);
                tb.SetText("Was suchst du hier? ..");
                yield return new WaitForSeconds(0.4f);
                tb.SetText("Was suchst du hier? ...");
                yield return new WaitForSeconds(1f);
                tb.Setup("Ich kann hier keine Stoerenfriede gebrauchen\n-_-", offset);
                yield return new WaitForSeconds(3.5f);
                tb.Setup("Weisst du was?", offset);
                yield return new WaitForSeconds(1.5f);
                tb.Setup("Ich glaub du wirst hier sowieso nicht durchkommen.", offset);
                yield return new WaitForSeconds(3.5f);
                tb.Setup("Bisher hat noch niemand meine Fallen geschlagen.", offset);
                yield return new WaitForSeconds(3.5f);
                tb.Setup("Gute deutsche Qualitaet versteht sich...", offset);
                GameHandler.watchedID1 = true;
                yield return new WaitForSeconds(2f);
                tb.Setup("Jedenfalls...", offset);
                yield return new WaitForSeconds(1f);
                tb.Setup("Ich muss noch was erledigen", offset);
                yield return new WaitForSeconds(3f);
                tb.Setup("Vielleicht sehen wir uns ja wieder...", offset);
                yield return new WaitForSeconds(2f);
                tb.Setup("Ich hoffe nicht.", offset);
                yield return new WaitForSeconds(2f);
                tb.Delete();
            }
            fade.StartFade(0.5f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.4f);

            PlayerMovement.blockMovement = false;

            MusicHandler.handler.Fade(1f, 0f, 1f);
        }
    }
}
