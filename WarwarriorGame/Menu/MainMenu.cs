using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LeaderboardModel;
using SDL2;
using WarwarriorGame;
using WarwarriorGame.Menu;
using WarwarriorGame.Network;

namespace WarwarriorGame
{
    class MainMenu : GameBase
    {
        public static string PlayerName = "not set";

        private Background background;
        private MenuUIRenderer menuRenderer;
        private int selectedLevel = 0;
        private const int NUM_LEVELS = 5;
        private AnimRenderer redAnimPlanet;
        private AnimRenderer blueAnimPlanet;

        private List<Score> allScores;

        public MainMenu(int windowWidth, int windowHeight) : base(windowWidth, windowHeight)
        {

        }

        protected override void Init()
        {
            base.Init();

            PlayerName = Utils.GeneratePlayerName();

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

        private async Task<List<Score>> LoadScores()
        {
            List<Score> allScores = await Leaderboard.LoadAllScores();

            for (int i = 0; i < allScores.Count; i++)
            {
                Console.WriteLine(allScores[i].player.user_name);
            }

            return allScores;
        }

        protected override void Load(IntPtr rendererPtr)
        {
            Task.Run(async () => allScores = await LoadScores());

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

            // Render player name
            menuRenderer.Render(rendererPtr, this, $"Name: {PlayerName}", new Vector2(50.0f, 50.0f), false);

            for (int i = 0; i < 5; i++)
            {
                menuRenderer.Render(rendererPtr, this, $"Level {i+1}", new Vector2(50.0f, WindowHeight - 100.0f - 40.0f * (5 - i)), i == selectedLevel);
            }

            menuRenderer.Render(rendererPtr, this, "Quit Game", new Vector2(50.0f, WindowHeight - 100.0f), selectedLevel == 5);

            float selectionHeight = WindowHeight - 100.0f - 40.0f * (5 - selectedLevel);
            redAnimPlanet.Render(rendererPtr, this, new Vector2(7.0f, selectionHeight));

            // Render scores for selected level
            RenderScores(rendererPtr);

            SDL.SDL_RenderPresent(rendererPtr);
        }

        private void RenderScores(IntPtr rendererPtr)
        {
            if (allScores is null)
            {
                menuRenderer.Render(rendererPtr, this, "Loading Scoreboard...", new Vector2(600.0f, 30.0f), false);
                return;
            }

            // DB indexing starts at 1
            List<Score> levelScores = allScores.Where(x => x.level.id == selectedLevel + 1).OrderByDescending(x => x.high_score).ToList();

            for (int i = 0; i < levelScores.Count; i++)
            {
                menuRenderer.Render(rendererPtr,
                    this,
                    $"{i + 1}: {levelScores[i].player.user_name} - {levelScores[i].high_score} - {levelScores[i].time_in_seconds}s",
                    new Vector2(450.0f, 30.0f + i * 50.0f),
                    false);
            }
        }
    }
}
