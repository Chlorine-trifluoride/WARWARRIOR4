using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SDL2;
using WarwarriorGame;
using WarwarriorGame.Menu;

namespace WarwarriorGame
{
    class MainMenu : GameBase
    {
        private Background background;
        private MenuUIRenderer menuRenderer;
        private int selectedLevel = 0;
        private const int NUM_LEVELS = 5;

        public MainMenu(int windowWidth, int windowHeight) : base(windowWidth, windowHeight)
        {

        }

        protected override void Init()
        {
            base.Init();

            background = new Background();
            menuRenderer = new MenuUIRenderer();
        }

        protected override void Cleanup()
        {
            menuRenderer.Cleanup();
            base.Cleanup();
        }

        protected override void Load(IntPtr rendererPtr)
        {
            background.LoadInit(rendererPtr, "assets/textures/stars01_brightstarts.png");
            menuRenderer.LoadInit(rendererPtr);
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (InputManager.GetKeyDownThisFrame(SDL.SDL_Keycode.SDLK_DOWN))
                selectedLevel++;

            if (InputManager.GetKeyDownThisFrame(SDL.SDL_Keycode.SDLK_UP))
                selectedLevel--;

            if (selectedLevel < 0)
                selectedLevel = 0;

            if (selectedLevel > NUM_LEVELS)
                selectedLevel = NUM_LEVELS;

            if (InputManager.GetKeyDownThisFrame(SDL.SDL_Keycode.SDLK_RETURN))
            {
                quit = true;

                if (selectedLevel != 5)
                    Program.SelectedLevel = selectedLevel;

                else // -1 signals to quit program
                    Program.SelectedLevel = -1;
            }

            base.UpdateLogic(deltaTime);
        }

        protected override void RenderScene(IntPtr rendererPtr)
        {
            SDL.SDL_RenderClear(rendererPtr);

            background.Render(rendererPtr, this);

            for (int i = 0; i < 5; i++)
            {
                menuRenderer.Render(rendererPtr, this, $"Level {i+1}", new Vector2(50.0f, WindowHeight - 100.0f - 40.0f * (5 - i)), i == selectedLevel);
            }

            menuRenderer.Render(rendererPtr, this, "Quit Game", new Vector2(50.0f, WindowHeight - 100.0f), selectedLevel == 5);

            SDL.SDL_RenderPresent(rendererPtr);
        }
    }
}
