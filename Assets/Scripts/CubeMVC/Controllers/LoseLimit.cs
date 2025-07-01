using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoseLimit : MonoBehaviour
{
    [SerializeField] private float delayBeforeLose = 3f;

    private Dictionary<Collider2D, Coroutine> _objectsInTrigger = new();

    private GamePlayController _playSceneController;
    private FinishGameController _finishGameController;

    public void Setup(GamePlayController gamePlayController, FinishGameController finishGameController)
    {
        _playSceneController = gamePlayController;
        _finishGameController = finishGameController;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BaseGamePlayElementView>(out var obj))
        {
            if (!_objectsInTrigger.ContainsKey(other))
            {
                Coroutine waitCoroutine = StartCoroutine(WaitAndLose(other));
                _objectsInTrigger.Add(other, waitCoroutine);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_objectsInTrigger.TryGetValue(other, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            _objectsInTrigger.Remove(other);
        }
    }

    private IEnumerator WaitAndLose(Collider2D obj)
    {
        yield return new WaitForSeconds(delayBeforeLose);

        _finishGameController.OnGameLose();
    }
}
