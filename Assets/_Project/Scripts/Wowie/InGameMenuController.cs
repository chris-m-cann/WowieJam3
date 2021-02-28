using System;
using UnityEngine;

namespace Wowie
{
    public class InGameMenuController : MonoBehaviour
    {
        enum MenuState
        {
            Game,
            Paused,
            Goal
        }

        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject goalMenu;
        [SerializeField] private GameObject backPannel;

        private MenuState _state = MenuState.Game;
        private float _previousScale = 1f;

        public void OnPlayerGoal()
        {
            backPannel.SetActive(true);
            pauseMenu.SetActive(false);
            goalMenu.SetActive(true);

            _state = MenuState.Goal;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (_state)
                {
                    case MenuState.Game:
                        Pause();
                        break;
                    case MenuState.Paused:
                        Play();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Play()
        {
            backPannel.SetActive(false);
            goalMenu.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = _previousScale;
            _state = MenuState.Game;
        }

        public void Pause()
        {
            _previousScale = Time.timeScale;
            Time.timeScale = 0f;

            backPannel.SetActive(true);
            goalMenu.SetActive(false);
            pauseMenu.SetActive(true);
            _state = MenuState.Paused;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}