using System.Collections.Generic;
using SDL2;

namespace WarwarriorGame
{
    static class InputManager
    {
        private static Dictionary<SDL.SDL_Keycode, bool> keyValuePairs;

        public static void Init()
        {
            keyValuePairs = new Dictionary<SDL.SDL_Keycode, bool>();
        }

        public static void SetKeyState(SDL.SDL_Keycode key, bool down)
        {
            keyValuePairs[key] = down;
        }

        public static bool GetKeyState(SDL.SDL_Keycode key)
        {
            if (keyValuePairs.ContainsKey(key))
                return keyValuePairs[key];

            return false;
        }
    }
}
