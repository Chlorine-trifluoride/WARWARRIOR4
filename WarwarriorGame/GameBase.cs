using System;
using System.Diagnostics;
using SDL2;

namespace WarwarriorGame
{
    abstract class GameBase
    {
        public int WindowWidth { get; protected set; }
        public int WindowHeight { get; protected set; }
        public uint DebugDelay { get; set; } = 0;

        private IntPtr rendererPtr;
        private IntPtr windowPtr;

        protected long elapsedMilliseconds = 0;
        protected bool quit = false;

        public GameBase(int windowWidth, int windowHeight)
        {
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
        }

        public void Run()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine("Unable to init SDL. Error: {0}", SDL.SDL_GetError());
                return;
            }

            windowPtr = SDL.SDL_CreateWindow("WARWARRIOR4", SDL.SDL_WINDOWPOS_UNDEFINED,
                SDL.SDL_WINDOWPOS_UNDEFINED, WindowWidth, WindowHeight,
                SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

            if (windowPtr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to init window. Error: {0}", SDL.SDL_GetError());
                return;
            }

            IntPtr iconImage = SDL_image.IMG_Load("assets/textures/player.png");
            SDL.SDL_SetWindowIcon(windowPtr, iconImage);

            // Init hardware accelerated graphics if possible, fall back on software
            rendererPtr = SDL.SDL_CreateRenderer(windowPtr, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (rendererPtr == IntPtr.Zero)
            {
                Console.WriteLine("Unable to init accelerated graphics. {0}", SDL.SDL_GetError());
                SDL.SDL_ClearError();
                Console.WriteLine("Fall back on software");

                rendererPtr = SDL.SDL_CreateRenderer(windowPtr, -1, SDL.SDL_RendererFlags.SDL_RENDERER_SOFTWARE);
                if (rendererPtr == IntPtr.Zero)
                {
                    Console.WriteLine("Unable to init software renderer. Error: {0}", SDL.SDL_GetError());
                    return;
                }

                DebugDelay = 16;
            }

            Init();
            Load(rendererPtr);

            Stopwatch stopwatch = new Stopwatch();
            long frames = 0;
            stopwatch.Start();
            long lastTime = stopwatch.ElapsedMilliseconds;

            while (!quit)
            {
                float deltaTime = (stopwatch.ElapsedMilliseconds - lastTime) / 1000.0f;
                lastTime = stopwatch.ElapsedMilliseconds;
                elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                HandleEvents();
                UpdateLogic(deltaTime);
                RenderScene(rendererPtr);
                frames++;

                SDL.SDL_Delay(DebugDelay); // used for testing frame rate independent code
            }

            stopwatch.Stop();
            long fps = frames / ((stopwatch.ElapsedMilliseconds / 1000) > 0 ? (stopwatch.ElapsedMilliseconds / 1000) : 1);
            Console.WriteLine($"Average FPS: {fps}");

            Cleanup();
        }

        private SDL.SDL_Event sdlEvent;
        private void HandleEvents()
        {
            try
            {
                while (SDL.SDL_PollEvent(out sdlEvent) != 0)
                {
                    switch (sdlEvent.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            break;

                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            InputManager.SetKeyState(sdlEvent.key.keysym.sym, true);
                            break;

                        case SDL.SDL_EventType.SDL_KEYUP:
                            InputManager.SetKeyState(sdlEvent.key.keysym.sym, false);
                            break;


                        case SDL.SDL_EventType.SDL_WINDOWEVENT:
                            if (sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED ||
                                sdlEvent.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED)
                            {
                                WindowWidth = sdlEvent.window.data1;
                                WindowHeight = sdlEvent.window.data2;
                            }
                            break;
                    }
                }
            }
            catch (Exception e) // hack
            {
                Console.WriteLine("Event exception: {0}", e.Message);
            }
        }

        protected virtual void Init()
        {
            InputManager.Init();
            SDL_ttf.TTF_Init();
        }

        protected virtual void Cleanup()
        {
            SDL_ttf.TTF_Quit();
            SDL.SDL_DestroyRenderer(rendererPtr);
            SDL.SDL_DestroyWindow(windowPtr);
            SDL.SDL_Quit();
        }

        protected virtual void UpdateLogic(float deltaTime)
        {
            // Used for testing frame rate independent code
            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_0))
                DebugDelay = 0;

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_1))
                DebugDelay = 3;

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_2))
                DebugDelay = 16;

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_3))
                DebugDelay = 30;

            if (InputManager.GetKeyState(SDL.SDL_Keycode.SDLK_ESCAPE))
                quit = true;

            InputManager.ResetFrameKeys();
        }

        protected abstract void Load(IntPtr rendererPtr);
        protected abstract void RenderScene(IntPtr rendererPtr);
    }
}
