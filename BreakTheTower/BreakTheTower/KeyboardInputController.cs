using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakTheTower
{
    internal class AbortGameException : Exception
    {
        public AbortGameException(string message) : base(message) { }
    }

    public class KeyboardInputController
    {
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        public KeyboardInputController()
        {
            // "Prime" ourselves so both keyboard state variables have something valid.
            UpdateKeyboardState();
            UpdateKeyboardState();
        }

        public void Update()
        {
            UpdateKeyboardState();

            if (WasKeyPressed(Keys.Escape))
            {
                throw new AbortGameException("Exiting Game...");
            }
        }

        public bool WasKeyPressed(Keys key)
        {
            return (Keyboard.GetState().IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key));
        }

        private void UpdateKeyboardState()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }
    }
}
