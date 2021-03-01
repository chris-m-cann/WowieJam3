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
        [SerializeField] private GameObject player;


        public void ToMainMenu()
        {
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
            player.SetActive(true);
        }

        public void ToCreditsMenu()
        {
            mainMenu.SetActive(false);
            player.SetActive(false);
            creditsMenu.SetActive(true);
        }
    }
}