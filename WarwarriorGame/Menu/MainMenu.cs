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
        private AnimRenderer redAnimPlanet;
        private AnimRenderer blueAnimPlanet;

        public MainMenu(int windowWidth, int windowHeight) : base(windowWidth, windowHeight)
        {

        }

        protected override void Init()
        {
            base.Init();

            background = new Background();
            menuRenderer = new MenuUIRenderer();
            redAnimPlanet = new AnimRenderer();
            blueAnimPlanet = new AnimRenderer();
        }

        protected override void Cleanup()
        {
            background.Cleanup();
            menuRenderer.Cleanup();
            redAnimPlanet.Cleanup();
            blueAnimPlanet.Cleanup();
            base.Cleanup();
        }

        protected override void Load(IntPtr rendererPtr)
        {
            background.LoadInit(rendererPtr, "assets/textures/stars01_brightstarts.png");
            menuRenderer.LoadInit(rendererPtr);
            redAnimPlanet.LoadInit(rendererPtr, "assets/textures/star_red01.png", 8, 8, 0.12f);
            blueAnimPlanet.LoadInit(rendererPtr, "assets/textures/star_blue03.png", 8, 8, 0.2f);
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

            // Random camera movement for background
            Camera.Position = new Vector2(MathF.Sin(SDL.SDL_GetTicks() / 3000.0f), MathF.Cos(SDL.SDL_GetTicks() / 2000.0f)) * 100.0f;

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

            float selectionHeight = WindowHeight - 100.0f - 40.0f * (5 - selectedLevel);
            redAnimPlanet.Render(rendererPtr, this, new Vector2(7.0f, selectionHeight));
            //blueAnimPlanet.Render(rendererPtr, this, new Vector2(WindowWidth / 2.0f, 300.0f));

            SDL.SDL_RenderPresent(rendererPtr);
        }
    }
}
