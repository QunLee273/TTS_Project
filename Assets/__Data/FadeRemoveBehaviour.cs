using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 1f;
    private float _timeElapsed;
    private SpriteRenderer _spriteRenderer;
    private GameObject _objRemove;
    private Color _startColor;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
        _objRemove = animator.gameObject.transform.parent.gameObject;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed += Time.deltaTime;
        
        float newAlpha = _startColor.a * (1 - (_timeElapsed / fadeTime));
        
        _spriteRenderer.color = new Color(_startColor.r, _startColor.g, _startColor.b, newAlpha);
        
        if (_timeElapsed >= fadeTime)
            Destroy(_objRemove);
    }
}
