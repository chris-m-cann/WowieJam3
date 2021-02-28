using System;
using UnityEngine;

namespace Wowie
{
    public class MainMenuController : MonoBehaviour
    {
        enum MenuState
        {
            Main,
            Credits
        }

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject creditsMenu;


        public void ToMainMenu()
        {
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void ToCreditsMenu()
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
    }
}