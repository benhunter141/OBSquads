using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public BaseUnit unit { get; private set; }
    public int healthPoints;

    public Health(BaseUnit _unit)
    {
        unit = _unit;
        healthPoints = unit.data.healthPoints;
    }


    public bool IsDead() => healthPoints <= 0;
    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        unit.healthBar.UpdateBar(healthPoints, unit.data.healthPoints);
        //show damage with floating kenney font numbers
        if (IsDead())
        {
            Die();
        }
    }
    void Die()
    {
        //killing blow red numbers
        //light anti-gravity
        //if last on team to die, Victory();
        unit.refHolder.rb.useGravity = false;
        float antigravForce = 40;
        unit.refHolder.rb.AddForce(Vector3.up * antigravForce);
        unit.refHolder.collider.enabled = false;
        unit.status = ServiceLocator.Instance.soManager.dead;
        ServiceLocator.Instance.unitManager.RetargetDeadUnit(unit);
        var renderers = unit.refHolder.AllRenderers();
        var canvasRenderers = unit.refHolder.AllCanvasRenderers();
        float fadeTime = 2;

        unit.StartCoroutine(FadeOutRenderers(renderers, fadeTime));
        unit.StartCoroutine(FadeOutRenderers(canvasRenderers, fadeTime));

        unit.StartCoroutine(DisableUnit(fadeTime));
        ServiceLocator.Instance.encounterManager.CheckForVictory();
    }


    IEnumerator FadeOutRenderers(List<Renderer> renderers, float fadeTime)
    {
        int frames = (int)(fadeTime * 30);
        float fraction = 1f / frames;
        for (int i = 0; i < frames; i++)
        {
            float alpha = 1f - fraction * i;
            foreach (var r in renderers)
            {
                Color color = r.material.color;
                color.a = alpha;
                r.material.color = color; //won't this same material fade on other units?
            }
            yield return Helpers.EndOfFrame;
        }
    }
    IEnumerator FadeOutRenderers(List<CanvasRenderer> renderers, float fadeTime)
    {
        int frames = (int)(fadeTime * 30);
        float fraction = 1f / frames;
        for (int i = 0; i < frames; i++)
        {
            float alpha = 1f - fraction * i;
            foreach (var r in renderers)
            {
                r.SetAlpha(alpha);
            }
            yield return Helpers.EndOfFrame;
        }
    }

    IEnumerator DisableUnit(float fadeTime)
    {
        int frames = (int)(fadeTime * 30);
        for (int i = 0; i < frames; i++)
        {
            yield return Helpers.EndOfFrame;
        }
        yield return new WaitForSeconds(fadeTime);
        unit.gameObject.SetActive(false);
    }

}
