using UnityEngine;
using Zenject;

public class InputController : MonoBehaviour
{
    [SerializeField] private Transform _dragArea;
    [SerializeField] private PlayInputButton _playButton;
    [SerializeField] private float _pushForce;
    [SerializeField] private Vector3 _pushDirrection;

    [Inject] private BaseSceneServiceProvider _sceneServiceProvider;
    private PlaySceneServiceProvider _playSceneServiceProvider;
    private CubeSpaner _cubeSpaner => _playSceneServiceProvider.CubeSpaner;
    private GamePlayController _gamePlayController => _playSceneServiceProvider.GameplayController;

    private InputState _currentInputState = InputState.NoInput;

    public void Initialize()
    {
        if (_sceneServiceProvider is PlaySceneServiceProvider playSceneServiceProvider)
        {
            _playSceneServiceProvider = playSceneServiceProvider;
        }
        else
        {
            Debug.LogError("GamePlayController not initialized, check DI");
        }

        _playButton.SetubButton(null, OnHoldDrag, OnHoldRelease);
    }

    public void ChangeState(InputState state)
    {
        _currentInputState = state;
    }

    private void OnHoldRelease()
    {
        var currentElement = _cubeSpaner.CurrentElement;

        if (_currentInputState == InputState.InputActive)
        {
            Debug.Log("Hold Release");

            currentElement?.Push(_pushDirrection, _pushForce);
            _currentInputState = InputState.NoInput;

            _gamePlayController.GoNextGameIteration();
        }
    }

    private void OnHoldDrag(Vector2 screenPos)
    {
        var currentElement = _cubeSpaner.CurrentElement;
        var draggedObject = currentElement.transform;

        if (_currentInputState == InputState.NoInput)
        {
            return;
        }

        // 1. Отримуємо Z-глибину об'єкта в камері
        float z = Camera.main.WorldToScreenPoint(draggedObject.position).z;

        // 2. Конвертуємо screenPos у worldPos
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, z));

        // 3. Обмеження межами DragArea
        Vector3 min = _dragArea.position - _dragArea.localScale / 2f;
        Vector3 max = _dragArea.position + _dragArea.localScale / 2f;

        Vector3 clampedPos = draggedObject.position;
        clampedPos.x = Mathf.Clamp(worldPos.x, min.x, max.x);
        clampedPos.y = Mathf.Clamp(worldPos.y, min.y, max.y);
        clampedPos.z = Mathf.Clamp(worldPos.z, min.z, max.z); // можеш закоментувати, якщо не треба

        // 4. Переміщення
        draggedObject.position = clampedPos;
    }
}
