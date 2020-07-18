using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AudioVisualizerWinFramework
{
    class NamedInputState
    {
        public enum InputState
        {
            [Description("Speaker Out")]
            SpeakerOut,
            [Description("Microphone In")]
            MicrophoneIn,
            [Description("From File")]
            FileIn
        }

        public InputState State { get; set; }
        public string Name { get; set; }

        public NamedInputState (InputState state, string name)
        {
            State = state;
            Name = name;
        }

        public static List<NamedInputState> FromInputStateEnum ()
        {
            List<NamedInputState> states = new List<NamedInputState>();
            foreach (InputState value in Enum.GetValues(typeof(InputState)))
            {
                string name = value.GetType().GetMember(value.ToString()).FirstOrDefault()
                                  ?.GetCustomAttribute<DescriptionAttribute>()?.Description
                                 ?? value.ToString();
                states.Add(new NamedInputState(value, name));
            }

            return states;
        }
    }
}
