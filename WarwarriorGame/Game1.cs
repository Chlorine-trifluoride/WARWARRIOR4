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
        private AnimatedStellarObject star3;
        private UI ui;
        private uint nextSpawn = 0;
        private Random random = new Random();

        public Game1(int windowWidth, int windowHeight) : base(windowWidth, windowHeight)
        {

        }

        protected override void Init()
        {
            base.Init();

            background = new Background();
            player = new Player(new Vector2(1008.0f, 808.0f), MathF.PI / 2);
            //enemy = new Enemy(new Vector2(300.0f, 300.0f), 0.0f);
            //star = new StellarBase(new Vector2(500.0f, 300.0f), 2000.0f);
            //star2 = new StellarBase(new Vector2(1300.0f, 850.0f), 700.0f);
            star3 = new AnimatedStellarObject(new Vector2(300.0f, 500.0f), 1000.0f);

            // Generate random stars
            for (int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    new StellarBase(new Vector2(1200.0f * x, 1200.0f * y), random.Next(1000, 3000));
                }
            }

            ui = new UI(player);
        }

        protected override void Cleanup()
        {
            player.Renderer.Cleanup();
            EnemyShipRenderer.Cleanup();
            StellarRenderer.Cleanup();
            AnimatedStellarRenderer.Cleanup();
            ui.Cleanup();

            base.Cleanup();
        }

        protected override void Load(IntPtr rendererPtr)
        {
            background.LoadInit(rendererPtr, "assets/textures/stars01_brightstarts.png");
            player.Renderer.LoadInit(rendererPtr, "assets/textures/playersheet.png");
            ParticleRenderer.LoadInit(rendererPtr, "assets/textures/PlayerFire.png");
            StellarRenderer.LoadInit(rendererPtr, "assets/textures/star01.png");
            AnimatedStellarRenderer.LoadInit(rendererPtr, "assets/textures/star_blue_fixed.png");
            ShieldRenderer.LoadInit(rendererPtr, "assets/textures/Shield.png");
            ShieldParticleRenderer.LoadInit(rendererPtr, "assets/textures/ShieldParticle.png");
            ExplosionParticleRenderer.LoadInit(rendererPtr, "assets/textures/ExplosionParticle.png");
            EnemyShipRenderer.LoadInit(rendererPtr, "assets/textures/enemysheet.png");
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
                player.Fire();
            }

            if (SDL.SDL_GetTicks() > nextSpawn)
            {
                Console.WriteLine("Spawning new enemy");
                new Enemy(Player.Inst.Position + Vector2.Random() * 1500.0f, 0.0f);

                nextSpawn = SDL.SDL_GetTicks() + 7000;
            }

            Camera.Update(player.Position, this, deltaTime);

            for (int i = 0; i < Actor.Actors.Count; i++)
            {
                Actor.Actors[i].Update(deltaTime);
            }

            for (int i = 0; i < Particle.Particles.Count; i++)
            {
                Particle.Particles[i].UpdateLogic(deltaTime);
            }

            ui.Update();
        }

        protected override void RenderScene(IntPtr rendererPtr)
        {
            SDL.SDL_RenderClear(rendererPtr);

            background.Render(rendererPtr, this);

            for (int i = 0; i < StellarBase.stellarObjects.Count; i++)
            {
                StellarBase.stellarObjects[i].Renderer.Render(rendererPtr, this);
            }

            for (int i = 0; i < Actor.Actors.Count; i++)
            {
                Actor.Actors[i].Renderer.Render(rendererPtr, this);
                Actor.Actors[i].Shield.Renderer.Render(rendererPtr, this);
            }

            for (int i = 0; i < Particle.Particles.Count; i++)
            {
                Particle.Particles[i].Renderer.Render(rendererPtr, this);
            }

            ui.Render(rendererPtr, this);

            SDL.SDL_RenderPresent(rendererPtr);
        }
    }
}
