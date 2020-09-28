using System;
using System.Collections.Generic;
using System.Threading;
using SDL2;
using WarwarriorGame;

namespace WarwarriorGame
{
    class Game1 : GameBase
    {
        private Background background;
        private Player player;
        private Enemy enemy;
        private StellarBase star;
        private StellarBase star2;
        private AnimatedStellarObject star3;
        private UI ui;

        private List<Projectile> projectiles = new List<Projectile>();

        public Game1(int windowWidth, int windowHeight) : base(windowWidth, windowHeight)
        {

        }

        protected override void Init()
        {
            base.Init();

            background = new Background();
            player = new Player(new Vector2(48.0f, 48.0f), MathF.PI / 2);
            enemy = new Enemy(new Vector2(300.0f, 300.0f), 0.0f);
            //star = new StellarBase(new Vector2(500.0f, 300.0f), 2000.0f);
            //star2 = new StellarBase(new Vector2(1300.0f, 850.0f), 700.0f);
            star3 = new AnimatedStellarObject(new Vector2(500.0f, 300.0f), 1000.0f);

            ui = new UI(player);
        }

        protected override void Cleanup()
        {
            player.Renderer.Cleanup();
            enemy.Renderer.Cleanup();
            ui.Cleanup();

            base.Cleanup();
        }

        protected override void Load(IntPtr rendererPtr)
        {
            background.LoadInit(rendererPtr, "assets/textures/stars01_brightstarts.png");
            player.Renderer.LoadInit(rendererPtr, "assets/textures/playersheet.png");
            enemy.Renderer.LoadInit(rendererPtr, "assets/textures/enemysheet.png");
            ParticleRenderer.LoadInit(rendererPtr, "assets/textures/PlayerFire.png");
            //star.Renderer.LoadInit(rendererPtr, "assets/textures/star01.png");
            //star2.Renderer.LoadInit(rendererPtr, "assets/textures/star01.png");
            star3.Renderer.LoadInit(rendererPtr, "assets/textures/star_blue_fixed.png");
            ui.LoadInit(rendererPtr);
        }

        protected override void UpdateLogic(float deltaTime)
        {
            base.UpdateLogic(deltaTime);

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_w))
                player.Accelerate();

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_s))
                player.Decelerate();

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_a))
                player.SteerLeft();

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_d))
                player.SteerRight();

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_z))
                player.TurnEngineOn();

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_x))
                player.TurnEngineOff();

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_SPACE))
            {
                Projectile p = new Projectile(player.Position + player.Renderer.GetCenter(), player.Heading, player.Rotation);
                projectiles.Add(p);
            }

            Camera.Update(player.Position, this, deltaTime);
            player.Update(deltaTime);
            enemy.Update(deltaTime);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].UpdateLogic(deltaTime);
            }

            ui.Update();
        }

        protected override void RenderScene(IntPtr rendererPtr)
        {
            SDL.SDL_RenderClear(rendererPtr);

            background.Render(rendererPtr, this);
            //star.Renderer.Render(rendererPtr, this);
            //star2.Renderer.Render(rendererPtr, this);
            star3.Renderer.Render(rendererPtr, this);
            enemy.Renderer.Render(rendererPtr, this);
            player.Renderer.Render(rendererPtr, this);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Renderer.Render(rendererPtr, this);
            }

            ui.Render(rendererPtr, this);

            SDL.SDL_RenderPresent(rendererPtr);
        }
    }
}
