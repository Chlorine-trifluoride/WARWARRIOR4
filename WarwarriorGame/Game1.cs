using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private uint difficulty;
        private Random random = new Random();

        public Game1(int windowWidth, int windowHeight, uint difficulty) : base(windowWidth, windowHeight)
        {
            this.difficulty = difficulty;
        }

        protected override void Init()
        {
            base.Init();

            background = new Background();
            player = new Player(new Vector2(1008.0f, 808.0f), MathF.PI / 2);
            star3 = new AnimatedStellarObject(new Vector2(300.0f, 500.0f), 1000.0f);

            // Generate random stars
            for (int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    new StellarBase(new Vector2(2700.0f * x, 2700.0f * y), random.Next(1300, 5000));
                }
            }

            ui = new UI();
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
            PlayerShipRenderer.LoadInit(rendererPtr, "assets/textures/playersheet.png");
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

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_r))// && Player.Inst.Shield.Remaining <= 0)
            {
                player = Player.GetRespawnNewPlayer();
            }

            if (SDL.SDL_GetTicks() > nextSpawn)
            {
                Vector2 spawnPos = Vector2.Random() * WindowWidth * 2.5f;
                
                while (Vector2.Distance(Player.Inst.Position, spawnPos) < WindowWidth)
                    spawnPos = Vector2.Random() * WindowWidth * 2.5f;

                new Enemy(spawnPos, (float)(random.NextDouble() * 2 * MathF.PI));
                nextSpawn = SDL.SDL_GetTicks() + 8000 / difficulty;
            }

            Camera.Update(player.Position, this, deltaTime);

            for (int i = 0; i < Actor.Actors.Count; i++)
            {
                Actor.Actors[i].Update(deltaTime);
            }

            Parallel.For(0, Particle.Particles.Count,
                i => {
                    // Don't update particles too far away
                    if (Utils.GetDistance(Particle.Particles[i].Position, Player.Inst.Position) > 2500.0f)
                        return;

                    Particle.Particles[i].UpdateLogic(deltaTime);
                });

            // remove particles marked for removal
            for (int i = 0; i < Particle.Particles.Count; i++)
            {
                if (Particle.Particles[i].MarkedForRemoval)
                    Particle.Particles.RemoveAt(i);
            }

            // remove actors marked for removal
            for (int i = 0; i < Actor.Actors.Count; i++)
            {
                if (Actor.Actors[i].MarkedForRemoval)
                    Actor.Actors.RemoveAt(i);
            }

            ui.Update(deltaTime);
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
